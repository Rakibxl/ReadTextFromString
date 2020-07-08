


































































































































































using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;


public class Program
{
    public class SamsungOrder

    {

        List<SamsungOrderRow> rowdata = new List<SamsungOrderRow>();

        public string OrderNumber { get; set; }

        public String OrderDate { get; set; }

        public string DistributorCode { get; set; }

        public string ExternelReference { get; set; }


        public List<SamsungOrderRow> GetSetOrderRows

        {

            set { rowdata = value; }

            get { return rowdata; }

        }

        public void OrderDetails()

        {


            Console.WriteLine(" OrderRows are:- ");

            foreach (SamsungOrderRow c in this.GetSetOrderRows)

            {
                Console.WriteLine(" SamsungOrder's OrderNumber:- " + this.OrderNumber);
                Console.WriteLine(" SamsungOrder's OrderDate:- " + this.OrderDate);
                Console.WriteLine(" SamsungOrder's DistributorCode:- " + this.DistributorCode);

                Console.WriteLine(" ArticleCode:->" + c.ArticleCode);
                Console.WriteLine(" Price:->" + c.Price);
                Console.WriteLine(" Quantity:->" + c.Quantity);
                Console.WriteLine(" Amount:->" + c.Amount);
                Console.WriteLine(" DeliveryDate:->" + c.DeliveryDate);
                Console.WriteLine(" SamsungOrder's ExternalReference:- " + this.ExternelReference);


                Console.WriteLine(" Another Row");
                Console.WriteLine("---------------------------------");


                // Here we can call SamsungOrderAdd to fill order item 

            }

            // Finally we can execute SamsungOrderJobSelect

        }

    }

    public class SamsungOrderRow

    {

        public List<SamsungOrderRow> getSetOrderRow { get; set; }

        public string ArticleCode { get; set; }
        public int Quantity { get; set; }
        public string Price { get; set; }
        public string Amount { get; set; }
        public string DeliveryDate { get; set; }

    }

    public static void Main()
    {
        List<SamsungOrderRow> List = new List<SamsungOrderRow>();

        SamsungOrderRow orderRow = new SamsungOrderRow();

        SamsungOrder samsungOrder = new SamsungOrder();


        var orderTxt = @"Purchase Order PO No : 5293274630 Date : 2020.03.16 Supplier Name & Address Buyer Name & Address L10IU1 : ECONOCOM INTERNATIONAL ITALIA S.P.A Samsung Electronics Italia S.p.A Via Marcello Nizzoli, 8 Via Mike Bongiorno, 9 MILANO 20124 Sales Person : 2061971 ESPRI Purchse Group : QSW Payment Terms : S160 Purchaser : AAAAA Currency : EUR Plant : S425 Destination : CIF / MILANO [SEI]Plant TO SITE : Use Code : I Material Qty Price Per Unit Amount Delivery Material Description Commercial-Code 1 MI-SCBSMT24SM 10 45.62 1 PC 456.20 2020.03.18 MI-SCBSMT24SM,, MI-SCBSMT24SM 2 MI-SCBSML24SM 10 32.28 1 PC 322.80 2020.03.18 MI-SCBSML24SM,, MI-SCBSML24SM Amount : 779.00 Remark 1 / 1";
        var results = orderTxt.Split(new string[] { "Purchase Order" }, StringSplitOptions.None);

        foreach (string result in results)
        {

            if (!string.IsNullOrEmpty(result))
            {

                //Console.WriteLine(result.Trim());
                //PO NO
                var OrderNumber = Regex.Match(result, @"(?<=PO No :)(\s\d{10}\s)(?=Date :)");
                //Console.WriteLine(OrderNumber.ToString().Trim());

                samsungOrder.OrderNumber = OrderNumber.ToString().Trim();

                //Date
                var OrderDate = Regex.Match(result, @"(?<=Date :)(\s\d{4}[.]\d{2}[.]\d{2}\s)(?=Supplier)");

                samsungOrder.OrderDate = OrderDate.ToString().Trim().Replace(".","-"); 

                //Console.WriteLine(Date.ToString().Trim());
                ////Supplier
                //var Supplier = Regex.Match(result, @"(?<=L10IU1 :)(.*)(?=Sales Person :)");
                //Console.WriteLine(Supplier.ToString().Trim());

                //Sales Person
                var SalesPerson = Regex.Match(result, @"(?<=Sales Person :)(\s\d{7}\s\w{5}\s)(?=Purchse Group :)");

                samsungOrder.DistributorCode = SalesPerson.ToString().Trim();


                //Console.WriteLine(SalesPerson.ToString().Trim());

                //OrderItemRow
                var OrderItemRowOne = Regex.Match(result, @"(?<=Commercial-Code)(.*)(?=Amount)");

                //Console.WriteLine(OrderItemRowOne.ToString().Trim());

                string[] TestString = Regex.Split(OrderItemRowOne.ToString(), @"(?<=\s|^)([0-9]+)(?=[\s]+[A-Z]*[-]+[A-Z0-9]*)");


                for (int i = 0; i < TestString.Length; i++)
                {
                    if (TestString[i] == " " || TestString[i] == null || TestString[i].Count() > 1)
                    {
                        continue;
                    }

                    if (TestString[i] == "1")
                    {
                        string[] OrderItemOne = TestString[i + 1].Trim().Split(' ');

                        orderRow.ArticleCode = OrderItemOne[0];
                        orderRow.Quantity = Convert.ToInt32(OrderItemOne[1]);
                        orderRow.Price = OrderItemOne[2];
                        orderRow.Amount = OrderItemOne[5];
                        orderRow.DeliveryDate = OrderItemOne[6].Replace(".", "-");

                        List.Add(orderRow);

                    }

                    if (TestString[i] == "2")
                    {
                        orderRow = new SamsungOrderRow();

                        string[] OrderItemTwo = TestString[i + 1].Trim().Split(' ');

                        orderRow.ArticleCode = OrderItemTwo[0];
                        orderRow.Quantity = Convert.ToInt32(OrderItemTwo[1]);
                        orderRow.Price = OrderItemTwo[2];
                        orderRow.Amount = OrderItemTwo[5];
                        orderRow.DeliveryDate = OrderItemTwo[6].Replace(".", "-");

                        List.Add(orderRow);

                    }

                }


                //string[] OrderItem = OrderItemRowOne.ToString().Trim().Split(' ');

                //orderRow.ArticleCode = OrderItem[1];
                //orderRow.Quantity = Convert.ToInt32(OrderItem[2]);
                //orderRow.Price = OrderItem[3];
                //orderRow.Amount = OrderItem[6];
                //orderRow.DeliveryDate = OrderItem[7].Replace(".", "-");

                //List.Add(orderRow);


                orderRow.getSetOrderRow = List;

                samsungOrder.GetSetOrderRows = orderRow.getSetOrderRow;

                samsungOrder.OrderDetails();

            }

        }
    }
}



