using System.Collections.Generic;

namespace GildedRose.Console
{
    class Program
    {
        IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program()
                          {
                              Items = new List<Item>
                                          {
                                              new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                                              new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                                              new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                                              new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                                              new Item
                                                  {
                                                      Name = "Backstage passes to a TAFKAL80ETC concert",
                                                      SellIn = 15,
                                                      Quality = 20
                                                  },
                                              new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
                                          }

                          };

            app.UpdateQuality();

            System.Console.ReadKey();

        }

        public void UpdateQuality()
        {
            for (var i = 0; i < Items.Count; i++)
            {
                //At the end of each day our system lowers both values for every item (Quality and SellIn):
                //"Sulfuras", being a legendary item, never has to be sold or decreases in Quality.
                if (Items[i].Name != "Sulfuras, Hand of Ragnaros")
                    Items[i].SellIn--;

                //The Quality of an item is never negative:
                if (Items[i].Quality >= 0 && Items[i].Quality <= 50)
                {
                    switch (Items[i].Name)
                    {
                        case "Aged Brie":
                            //"Aged Brie" actually increases in Quality the older it gets:
                            Items[i].Quality++;
                            break;
                        case "Sulfuras, Hand of Ragnaros":
                            break;
                        case "Backstage passes to a TAFKAL80ETC concert":
                            //"Backstage passes", like aged brie, increases in Quality as it's SellIn 
                            //value approaches; Quality increases by 2 when there are 10 days or less
                            //and by 3 when there are 5 days or less but Quality drops to 0 after the 
                            //concert:
                            if (Items[i].SellIn == 0)
                            {
                                Items[i].Quality = 0;
                            }
                            else if (Items[i].SellIn <= 5)
                            {
                                //If quality is 50 then don't increment, else if 49 then increment by 1, else increment 2.
                                Items[i].Quality += Items[i].Quality == 50 ? 0 : Items[i].Quality == 49 ? 1 : 2;
                            }
                            else if (Items[i].SellIn <= 10)
                            {
                                //If quality is 50 then don't increment, else if 49 then increment by 1, and if 48 add 2.
                                //This will make sure quality never exceeds 50!
                                Items[i].Quality += Items[i].Quality == 50 ? 0 : Items[i].Quality == 49 ? 1 : Items[i].Quality == 48 ? 2 : 3;
                            }
                            else
                            {
                                Items[i].Quality += Items[i].Quality == 50 ? 0 : 1;
                            }
                            break;
                        case "Conjured Mana Cake":
                            //"Conjured" items degrade in Quality twice as fast as normal items:
                            Items[i].Quality -= Items[i].Quality == 0 ? 0 : Items[i].Quality == 1 ? 1 : 2;
                            break;
                        default:
                            //Once the sell by date has passed, Quality degrades twice as fast:
                            //This statement also blocks quality from becoming negative.
                            Items[i].Quality -= Items[i].Quality == 0 ? 0 : Items[i].Quality == 1 ? 1 : (Items[i].SellIn < 0 ? 2 : 1);
                            break;
                    }
                }
                else
                {
                    //AS Quantity cannot be negative and cannot be more than 50, we must check and set it:
                    if (Items[i].Quality > 50)
                        Items[i].Quality = 50;

                    if (Items[i].Quality < 0)
                        Items[i].Quality = 0;
                }
            }
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
