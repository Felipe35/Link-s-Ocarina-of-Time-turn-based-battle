using System;

public class LinkWorld
{
    public static void Main(string[] args)
    {
        Console.WriteLine("======*WELLCOME TO LINK'S OCARINA OF TIME'S BOSSES MATCH!*======");

        Console.WriteLine("*---read the instructions before began to the match---*");
        Console.WriteLine("Link as some stats that keep's him alive and others to defense him \n" +
                "self with his shield, sword to produce damage, energy to improve attacks, that accumulates\n" +
                ", and block that blocks damage income.\n" +
                "Link's: based stats: health: 100 - energy: 30: - block is in %: 0.20 - damage: 15\n" +
                "1. Health: Can be recuperate by Health Potion: Use linkUseHealthPotion()\n" +
                "2. Energy: Can be increased up to 30 and decrease every time by 10, decreasing the booster: Use linkEnchantWeaponWithEnergy()\n" +
                "3. Block: Everytime is use, the block value increase, up to 60% and reduce incoming damage: Use linkIncreaseBlockChance() \n" +
                "4. Have fun finding others skills in the Game!\n" +
                "5. Bosses drop items, therefore pay attention to bosses hint");
        Console.WriteLine("====*Bosses Hint*====\n" +
                "1. When you enhance or enchant, you can awaken the anger of others.\n" +
                "2. The iron does not have effect against a iron, there is a way to blow up things!");
        Console.WriteLine();
        Console.WriteLine();

        // Load Link
        double[] link = linkInit(100, 30, 0.20, 15);

        // Link Init Bag
        string[] bag = initBag(4);
        bag[0] = "Health Potion";
        bag[1] = "";
        bag[2] = "";
        bag[3] = "";

        // Load Queen Gohma Boss
        double[] queen = queenGohmaInit(150, 30, 0.0);
        string[] queenLoot = queenGohmaLootInit(2);
        queenLoot[0] = "Fire Bomb";
        queenLoot[1] = "Health Potion";

        // Load King Dodongo Boss
        double[] king = kingDodongoInit(50, 40, 1.0);
        string[] kingLoot = kingDodongoLoot(1);
        kingLoot[0] = "Electric Protector";

        bool isQueenDefetead = queenCombat(link, queen, bag, queenLoot);
        if (!isQueenDefetead)
        {
            Console.WriteLine("=== Game Over ===");

        }
        Console.WriteLine("=== Moving to next boss: King Dodongo! ===");
        // reset Link for king fight
        link = linkInit(100, 30, 0.20, 15);

        bool isKingDefetead = kingCombat(link, king, bag, kingLoot);
        if (!isKingDefetead)
        {
            Console.WriteLine("=== Game Over ===");
        }


        Console.WriteLine("==== End of Story for now! ====");
    }

    // -------- LINK INIT & STATS --------

    private static int linkHealthInit(int newHealth)
    {
        return newHealth;
    }

    private static int linkEnergyInit(int energy)
    {
        return energy;
    }

    private static int linkSwordInit(int sword)
    {
        return sword;
    }

    private static double linkShieldInit(double shield)
    {
        return shield;
    }

    public static string[] initBag(int size)
    {
        return new string[size];
    }

    public static double[] linkInit(int hp, int ene, double block, int damage)
    {
        int health = linkHealthInit(hp);
        int energy = linkEnergyInit(ene);
        double shield = linkShieldInit(block);
        int sword = linkSwordInit(damage);
        double[] link = new double[4];

        link[0] = health;
        link[1] = energy;
        link[2] = shield;
        link[3] = sword;

        return link;
    }

    public static void linkAttackQueen(double[] link, double[] queen)
    {
        queen[0] -= (int)link[3];
    }

    public static void linkAttackKing(double[] link, double[] king)
    {
        // Si ya llegó a 100% → inmune
        if (king[2] >= 1.0)
        {
            Console.WriteLine("King Dodongo es inmune (100% armor)");
            return;
        }
        double baseDamage = link[3];                     // daño de Link
        double finalDamage = baseDamage * (1 - king[2]);

        king[0] -= finalDamage;
    }

    public static void linkIncreaseBlockChance(double[] link)
    {
        if (link[2] < 0.60)
        {
            link[2] += 0.20;
        }
    }

    public static void linkUseHealthPotion(double[] link, string[] potion)
    {
        for (int i = 0; i < potion.Length; i++)
        {
            if (potion[i] == "Health Potion")
            {
                if (link[0] < 100)
                {
                    link[0] = 100;
                    potion[i] = "Slot Potion is Empty!";
                    break;
                }
            }
        }
    }

    public static void linkUseFireBombOnKing(double[] link, string[] potion, double[] king)
    {
        for (int i = 0; i < potion.Length; i++)
        {
            if (potion[i] == "Fire Bomb")
            {
                king[2] -= 0.80;
                potion[i] = "Slot Fire Bomb is Empty!";
                break;
            }
        }
    }

    public static void linkEnchantWeaponWithEnergy(double[] link)
    {
        if (link[1] <= 30)
        {
            double increaseDmg = ((link[3] * link[1]) / link[1]) + link[3];
            link[3] += increaseDmg;
            link[1] -= 10;
        }
    }

    public static void linkStatus(double[] link, string[] bag)
    {
        // imprimir stats
        string border = "+--------------------------------------+";

        Console.WriteLine(border);
        Console.WriteLine("| {0,-38}|", "LINK STATUS");
        Console.WriteLine(border);

        string line;

        line = string.Format("Health: {0:F2}", link[0]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Energy: {0:F2}", link[1]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Block Value: {0:F2}", link[2]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Damage: {0:F2}", link[3]);
        Console.WriteLine("| {0,-38}|", line);

        Console.WriteLine(border);

        // Bag
        Console.WriteLine("| {0,-38}|", "BAG");
        Console.WriteLine(border);

        if (bag == null || bag.Length == 0)
        {
            Console.WriteLine("| {0,-38}|", "(Bag empty)");
        }
        else
        {
            for (int i = 0; i < bag.Length; i++)
            {
                line = string.Format("[{0}] {1}", i, bag[i]);
                Console.WriteLine("| {0,-38}|", line);
            }
        }

        Console.WriteLine(border);
    }

    // -------- QUEEN GOHMA --------

    private static int queenGohmaHealthInit(int health)
    {
        return health;
    }

    private static int queenGohmaDamageInit(int damage)
    {
        return damage;
    }

    private static double queenGohmaRageInit(double rage)
    {
        return rage;
    }

    public static string[] queenGohmaLootInit(int size)
    {
        return new string[size];
    }

    private static double queenGohmaRage(double currentRage)
    {
        return currentRage + 4;
    }

    public static void queenGohmaAttackChargedByRage(double[] link, double[] queen)
    {
        double mitigation = queen[1] * link[2];
        queen[2] = queenGohmaRage(queen[2]);
        queen[1] += queen[2];
        double finalDamage = queen[1] - mitigation;
        link[0] -= (int)finalDamage;
    }

    public static void queenGohmaAttack(double[] link, double[] queen)
    {
        double mitigation = queen[1] * link[2];
        double finalDamage = queen[1] - mitigation;
        link[0] -= (int)finalDamage;

        if ((Math.Round(link[2] * 100.0) / 100.0) == 0.60)
        {
            link[2] -= 0.20;
        }
    }

    public static double[] queenGohmaInit(int hp, int dmg, double rage)
    {
        int health = queenGohmaHealthInit(hp);
        int damage = queenGohmaDamageInit(dmg);
        double rg = queenGohmaRageInit(rage);

        double[] enemy = new double[3];
        enemy[0] = health;
        enemy[1] = damage;
        enemy[2] = rg;
        return enemy;
    }

    public static void queenGohmaStatus(double[] enemy)
    {
        string border = "+--------------------------------------+";

        Console.WriteLine(border);
        Console.WriteLine("| {0,-38}|", "QUEEN GOHMA STATUS");
        Console.WriteLine(border);

        string line;

        line = string.Format("Health: {0:F2}", enemy[0]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Damage: {0:F2}", enemy[1]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Rage: {0:F2}", enemy[2]);
        Console.WriteLine("| {0,-38}|", line);

        Console.WriteLine(border);
    }

    // -------- KING DODONGO --------

    private static int kingDodongoHealthInit(int health)
    {
        return health;
    }

    private static int kingDodongoDamageInit(int damage)
    {
        return damage;
    }

    public static string[] kingDodongoLoot(int size)
    {
        return new string[size];
    }

    private static double kingDodongoArmorInit(double armor)
    {
        return armor;
    }

    public static void kingDodongoAttack(double[] link, double[] king)
    {
        double mitigation = king[1] * link[2];
        double finalDamage = king[1] - mitigation;
        link[0] -= (int)finalDamage;

        if ((Math.Round(link[2] * 100.0) / 100.0) == 0.60)
        {
            link[2] -= 0.20;
        }
    }

    public static double[] kingDodongoInit(int hp, int dmg, double armor)
    {
        int health = kingDodongoHealthInit(hp);
        int damage = kingDodongoDamageInit(dmg);
        double amo = kingDodongoArmorInit(armor);

        double[] enemy = new double[3];
        enemy[0] = health;
        enemy[1] = damage;
        enemy[2] = amo;
        return enemy;
    }

    public static void kingDoDongoStatus(double[] enemy)
    {
        string border = "+--------------------------------------+";

        Console.WriteLine(border);
        Console.WriteLine("| {0,-38}|", "KING DODONGO STATUS");
        Console.WriteLine(border);

        string line;

        line = string.Format("Health: {0:F2}", enemy[0]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Damage: {0:F2}", enemy[1]);
        Console.WriteLine("| {0,-38}|", line);

        line = string.Format("Armor Percentage: {0:F2}", enemy[2] * 100);
        Console.WriteLine("| {0,-38}|", line);

        if (enemy[2] < 1.0)
        {
            line = "Part of his amor is been destroyed!!";
            Console.WriteLine("| {0,-38}|", line);
        }
        else
        {
            line = "His armor is like a rock!";
            Console.WriteLine("| {0,-38}|", line);
        }



        Console.WriteLine(border);
    }

    // -------- COMBATS --------

    public static bool queenCombat(double[] link, double[] queen, string[] bag, string[] queenLoot)
    {
        Console.WriteLine("=== THE MATCH BEGAN! ===");
        Console.WriteLine("!!!!==== Queen Gohman Boss====!!!!");

        while (link[0] > 0 && queen[0] > 0)
        {
            Console.WriteLine("===Link's Menu===");
            Console.WriteLine(
                    "\tPress 1 to attack\n" +
                    "\tPress 2 to block\n" +
                    "\tPress 3 to enchant weapon\n" +
                    "\tPress 4 to use health potion\n" +
                    "\tPress 5 to open status combat");

            Console.Write("Enter option: ");
            string input = Console.ReadLine();

            try
            {   int choice = int.Parse(input);
                switch (choice)
                {
                    case 1:
                        linkAttackQueen(link, queen);
                        queenGohmaAttack(link, queen);
                        Console.WriteLine("\tYou attacked Queen boss with: " + link[3] + " damage!");
                        Console.WriteLine("\tQueen attacked you with: " + queen[1] + " damage!");
                        break;
                    case 2:
                        linkIncreaseBlockChance(link);
                        queenGohmaAttack(link, queen);
                        Console.WriteLine("\tYou increase block value by: " +
                                          string.Format("{0:F2}%", link[2] * 100) +
                                          " current block value");
                        Console.WriteLine("\tQueen attacked you with: " + queen[1] + " damage!");
                        break;
                    case 3:
                        linkEnchantWeaponWithEnergy(link);
                        queenGohmaAttackChargedByRage(link, queen);
                        Console.WriteLine("\tYou enchant your weapon and your damage increase by!: " +
                                          link[3] + " and your energy decrease by: " + link[1]);
                        Console.WriteLine("\tOh! Queen now is getting mad!, she increase damage by: " + queen[1] + " damage!");
                        break;
                    case 4:
                        linkUseHealthPotion(link, bag);
                        queenGohmaAttack(link, queen);
                        Console.WriteLine("\tYour health now is restored! and you have no items left");
                        Console.WriteLine("\tQueen attacks you with: " + queen[1] + " damage");
                        break;
                    case 5:
                        linkStatus(link, bag);
                        queenGohmaStatus(queen);
                        break;
                }
            }
            catch (FormatException)
            {
                 Console.WriteLine($"Error: '{input}' no es un número válido.");
            }

        }

        if (link[0] <= 0 && queen[0] <= 0)
        {
            Console.WriteLine("Both Link and Queen are down! It's a draw!");
            return false;
        }
        else if (link[0] <= 0)
        {
            Console.WriteLine("Link has been defeated...");
            return false;
        }
        else if (queen[0] <= 0)
        {
            Console.WriteLine("You defeated Queen Gohma!! two items has been placed in your bag");
            Console.WriteLine("Your health increase 25 points and all stats have been reset");
            bag[1] = queenLoot[0];
            bag[2] = queenLoot[1];
            return true;
        }

        return false;
    }

    public static bool kingCombat(double[] link, double[] king, string[] bag, string[] kingLoot)
    {
        Console.WriteLine("=== THE MATCH BEGAN! ===");
        Console.WriteLine("!!!!==== King Dodongo Boss ====!!!!");

        while (link[0] > 0 && king[0] > 0)
        {
            Console.WriteLine("===Link's Menu===");
            Console.WriteLine(
                    "\tPress 1 to attack\n" +
                    "\tPress 2 to block\n" +
                    "\tPress 3 to enchant weapon\n" +
                    "\tPress 4 to use health potion\n" +
                    "\tPress 5 to use fire bomb\n" +
                    "\tPress 6 to open status combat");

            Console.Write("Enter option: ");
            string input = Console.ReadLine();
            try
            {
                int choice = int.Parse(input);
                switch (choice)
                {
                    case 1:
                        linkAttackKing(link, king);
                        kingDodongoAttack(link, king);
                        Console.WriteLine("\tYou attacked King boss with: " + link[3] + " damage!");
                        if (king[2] < 1.0)
                        {
                            Console.WriteLine("\tKing now is vulnerable to physical damage!");
                        }
                        else
                        {
                            Console.WriteLine("\tBut has not effect to King boss, and you received damage of: " + king[1]);
                        }

                        break;
                    case 2:
                        linkIncreaseBlockChance(link);
                        kingDodongoAttack(link, king);
                        Console.WriteLine("\tYou increase block value by: " +
                                          string.Format("{0:F2}%", link[2] * 100) +
                                          " current block value");
                        Console.WriteLine("\tKing attacks you with: " + king[1] + " damage!");
                        break;
                    case 3:
                        linkEnchantWeaponWithEnergy(link);
                        kingDodongoAttack(link, king);
                        Console.WriteLine("\tYou enchant your weapon and your damage increase by!: " +
                                          link[3] + " and your energy decrease by: " + link[1]);
                        Console.WriteLine("\tKing attacks you with: " + king[1] + " damage!");
                        break;
                    case 4:
                        linkUseHealthPotion(link, bag);
                        kingDodongoAttack(link, king);
                        Console.WriteLine("\tYour health now is restored! and you have no items left");
                        Console.WriteLine("\tKing attacks you with: " + king[1] + " damage!");
                        break;
                    case 5:
                        linkUseFireBombOnKing(link, bag, king);
                        Console.WriteLine("\tThat's is super effective against King!");
                        Console.WriteLine("\tNow King's armor is been reduce by: " +
                                          string.Format("{0:F2}", king[2] * 100));
                        break;
                    case 6:
                        linkStatus(link, bag);
                        kingDoDongoStatus(king);
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine($"Error: '{input}' no es un número válido.");
            }



        }

        if (link[0] <= 0 && king[0] <= 0)
        {
            Console.WriteLine("Both Link and King are down! It's a draw!");
            return false;
        }
        else if (link[0] <= 0)
        {
            Console.WriteLine("Link has been defeated...");
            return false;
        }
        else if (king[0] <= 0)
        {
            Console.WriteLine("You defeated King Dodongo!");
            return true;
        }

        return false;
    }

}