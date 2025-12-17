namespace PMP_LAB09
{
    internal class GameLogic
    {
        Game game;

        public GameLogic(Game game)
        {
            this.game = game;
        }

        void CleanUpGameItems()
        {
            if (game.Items.Count == 0)
            {
                return;
            }

            List<GameItem> availableItems = [];
            for (int i = 0; i < game.Items.Count; i++)
            {
                if (game.Items[i].Available)
                {
                    availableItems.Add(game.Items[i]);
                }
            }

            game.Items.Clear();
            game.Items.AddRange(availableItems);
        }

        void CleanUpDemons()
        {
            if (game.Demons.Count == 0)
            {
                return;
            }

            List<Demon> aliveDemons = [];
            for (int i = 0; i < game.Demons.Count; i++)
            {
                if (game.Demons[i].Alive)
                {
                    aliveDemons.Add(game.Demons[i]);
                }
            }

            game.Demons.Clear();
            game.Demons.AddRange(aliveDemons);
        }

        List<GameItem> GetGameItemsWithinDistance(Position position, double distance)
        {
            List<GameItem> itemsInDistance = [];
            for (int i = 0; i < game.Items.Count; i++)
            {
                if (Position.Distance(position, game.Items[i].Position) <= distance)
                {
                    itemsInDistance.Add(game.Items[i]);
                }
            }

            return itemsInDistance;
        }

        List<Demon> GetDemonsWithinDistance(Position position, double distance)
        {
            List<Demon> demonsInDistance = [];
            for (int i = 0; i < game.Demons.Count; i++)
            {
                if (Position.Distance(position, game.Demons[i].Position) <= distance)
                {
                    demonsInDistance.Add(game.Demons[i]);
                }
            }

            return demonsInDistance;
        }

        double GetTotalFillingRatio(Position position)
        {
            double total = 0;
            List<GameItem> itemsInDistance = GetGameItemsWithinDistance(position, 0);
            for (int i = 0; i < itemsInDistance.Count; i++)
            {
                total += itemsInDistance[i].FillingRatio;
            }

            List<Demon> demonsInDistance = GetDemonsWithinDistance(position, 0);
            for (int i = 0; i < demonsInDistance.Count; i++)
            {
                total += demonsInDistance[i].FillingRatio;
            }

            return total;
        }

        void DemonMoveLogic(Demon demon, long dt)
        {
            int x = demon.Position.X + Random.Shared.Next(-1, 2);
            int y = demon.Position.Y + Random.Shared.Next(-1, 2);
            Position targetPosition = new(x, y);
            Move(demon, targetPosition, dt);
        }

        public void PlayerAttackLogic()
        {
            if (game.Player.Ammo <= 0)
            {
                return;
            }

            game.Player.Shoot();
            game.PlaySoundEffect(SoundEffectType.Shotgun);

            List<Demon> demonsInSight = GetDemonsWithinDistance(game.Player.Position, game.Player.SightRange);
            for (int i = 0; i < demonsInSight.Count; i++)
            {
                Demon demon = demonsInSight[i];
                demon.TakeDamage(CalculateDamage(game.Player.Position, demon.Position, game.Player.GetDamage()));

                if (!demon.Alive)
                {
                    int points = 1;
                    switch (demon.Type)
                    {
                        case DemonType.Imp:
                            points = 3;
                            break;
                        case DemonType.Mancubus:
                            points = 10;
                            break;
                    }

                    game.Player.AddCombatPoints(points);
                }
            }
        }

        public void PlayerDirectInteractionLogic()
        {
            List<GameItem> items = GetGameItemsWithinDistance(game.Player.Position, 1);
            for (int i = 0; i < items.Count; i++)
            {
                GameItem item = items[i];
                switch (item.Type)
                {
                    case ItemType.Door:
                        item.Interact();
                        game.PlaySoundEffect(SoundEffectType.Door);
                        break;
                    case ItemType.LevelExit:
                        game.PlayMusic("sounds/level_complete.mp3");
                        item.Interact();
                        game.Exited = true;
                        break;
                }
            }
        }

        public void PlayerIndirectInteractionLogic()
        {
            List<GameItem> items = GetGameItemsWithinDistance(game.Player.Position, 0);
            for (int i = 0; i < items.Count; i++)
            {
                GameItem item = items[i];
                switch (item.Type)
                {
                    case ItemType.Ammo:
                        if (game.Player.Ammo < game.Player.MaxAmmo)
                        {
                            game.PlaySoundEffect(SoundEffectType.ItemPickup);
                            game.Player.PickUpAmmo(5);
                            item.Available = false;
                        }
                        break;
                    case ItemType.BFGCell:
                        if (game.Player.BFGCells < game.Player.MaxBFGCells)
                        {
                            game.PlaySoundEffect(SoundEffectType.ItemPickup);
                            game.Player.PickUpBFGCell();
                            item.Available = false;
                        }
                        break;
                    case ItemType.Medikit:
                        if (game.Player.Health < game.Player.MaxHealth)
                        {
                            game.PlaySoundEffect(SoundEffectType.ItemPickup);
                            game.Player.PickUpHealth(25);
                            item.Available = false;
                        }
                        break;
                    case ItemType.ToxicWaste:
                        game.PlaySoundEffect(SoundEffectType.Pain);
                        game.Player.TakeDamage(5);
                        break;
                }
            }
        }

        public void PlayerBFGAttackLogic()
        {
            if (game.Player.BFGCells <= 0)
            {
                return;
            }

            game.Player.ShootBFG();
            game.PlaySoundEffect(SoundEffectType.BFG);

            List<Demon> demonsInSight = GetDemonsWithinDistance(game.Player.Position, game.Player.SightRange);
            for (int i = 0; i < demonsInSight.Count; i++)
            {
                Demon demon = demonsInSight[i];
                //demon.TakeDamage(CalculateDamage(game.Player.Position, demon.Position, game.Player.GetBFGDamage()));
                demon.TakeDamage(game.Player.GetBFGDamage());

                if (!demon.Alive)
                {
                    game.Player.AddCombatPoints(1);
                }
            }
        }

        void DemonAttackLogic(Demon demon, long dt)
        {
            double distance = Position.Distance(game.Player.Position, demon.Position);
            if (distance <= demon.AttackRange)
            {
                game.PlaySoundEffect(SoundEffectType.Pain);
                game.Player.TakeDamage(CalculateDamage(distance, demon.GetDamage()) * (int)dt/1000);
            }
        }

        void DemonIndirectInteractionLogic(Demon demon)
        {
            List<GameItem> items = GetGameItemsWithinDistance(demon.Position, 0);
            for (int i = 0; i < items.Count; i++)
            {
                GameItem item = items[i];
                switch (item.Type)
                {
                    case ItemType.ToxicWaste:
                        demon.TakeDamage(5);
                        break;
                }
            }
        }

        void UpdateDemons(long dt)
        {
            for (int i = 0; i < game.Demons.Count; i++)
            {
                Demon demon = game.Demons[i];
                demon.UpdateState(game.Player);
                switch (demon.State)
                {
                    case DemonStateType.Move:
                        DemonMoveLogic(demon, dt);
                        break;
                    case DemonStateType.Attack:
                        DemonAttackLogic(demon, dt);
                        break;
                }

                DemonIndirectInteractionLogic(demon);
            }
        }

        public void UpdateGameState(long dt)
        {
            UpdateDemons(dt);
            CleanUpGameItems();
            CleanUpDemons();
        }

        public void Move(Player player, Position position)
        {
            double totalFill = GetTotalFillingRatio(position) + player.FillingRatio;
            if (totalFill < 1)
            {
                player.Position = position;
            }
        }

        public void Move(Demon demon, Position position, long dt)
        {
            double chance = demon.Speed / 100.0 * dt / 1000.0;
            if (Random.Shared.NextDouble() >= chance) { return; }

            double totalFill = GetTotalFillingRatio(position) + demon.FillingRatio;
            if (totalFill < 1)
            {
                demon.Position = position;
            }
        }

        

        static int CalculateDamage(Position attackerPosition, Position victimPosition, int damage)
        {
            return (int)(2 * damage / (1 + Position.Distance(attackerPosition, victimPosition)));
        }

        static int CalculateDamage(double distance, int damage)
        {
            return (int)(2 * damage / (1 + distance));
        }
    }
}
