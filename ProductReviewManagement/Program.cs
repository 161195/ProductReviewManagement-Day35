using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Data;

namespace ProductReviewManagement
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //UC1
            //Collection initializer
            List<ProductReview> list = new List<ProductReview>()
            {
                new ProductReview(){ ProductId=1,UserId=1,Review="good",Rating=17,IsLike=true},
                new ProductReview(){ ProductId=1,UserId=3,Review="bad",Rating=1,IsLike=false},
                new ProductReview(){ ProductId=3,UserId=5,Review="good",Rating=20,IsLike=true},
                new ProductReview(){ ProductId=4,UserId=7,Review="average",Rating=10,IsLike=true},
                new ProductReview(){ ProductId=5,UserId=1,Review="bad",Rating=5,IsLike=false},
                new ProductReview(){ ProductId=6,UserId=4,Review="bad",Rating=4,IsLike=false}
            };
            RetrieveTop3RecordsFromList(list);
            Console.ReadLine();
            Console.WriteLine("Records based on rating and product id : ");
            RetrieveRecordsBasedOnRatingAndProductId(list);
            Console.ReadLine();
            Console.WriteLine("Count of ProductId : ");
            CountingProductID(list);
            Console.WriteLine("ProductID and Review are: ");
            RetrieveProductIDAndReview(list);
            Console.WriteLine("Remaining are: ");
            SkipTop5RecordsFromListAndRetrieveOtherData(list);
            Console.WriteLine("Retrieved product id and reviews are: ");
            RetrieveProductIDAndReview1(list);

            CreateDataTable();



        }
        //UC2
        //This method for retrieve top three records from list
        public static void RetrieveTop3RecordsFromList(List<ProductReview> list)
        {
            //Query syntax for LINQ 
            var result = from product in list orderby product.Rating descending select product;
            var topThreeRecords = result.Take(3);
            foreach (ProductReview product in topThreeRecords)
            {
                Console.WriteLine("ProductId : " + product.ProductId + " UserId : " + product.UserId + " Rating : " + product.Rating + " Review : " + product.Review + " IsLike : " + product.IsLike);
            }
        }
        //UC3
        //This method for retrieve the records based on rating and product ID.      
        public static void RetrieveRecordsBasedOnRatingAndProductId(List<ProductReview> list)
        {
            //Method syntax for LINQ
            var data = (list.Where(r => r.Rating > 3 && (r.ProductId == 1 || r.ProductId == 4 || r.ProductId == 9))).ToList();
            foreach (var element in data)
            {
                Console.WriteLine("ProductId : " + element.ProductId + " Rating : " + element.Rating + " UserId : " + element.UserId + " Review : " + element.Review + " IsLike : " + element.IsLike);
            }
        }
        //UC-4
        //counting each ID present in the list
        public static void CountingProductID(List<ProductReview> list)
        {
            //Method syntax for LINQ
            var data = list.GroupBy(p => p.ProductId).Select(x => new { ProductId = x.Key, count = x.Count() });
            foreach (var element in data)
            {
                Console.WriteLine("ProductId : " + element.ProductId + " \t " + "Count" + element.count);
                Console.WriteLine("-----------------------");
            }
        }
        //UC-5
        //Retrive product ID and review present in the list
        public static void RetrieveProductIDAndReview(List<ProductReview> list)
        {
            //using select method
            var p = list.Select(product => new { ProductId = product.ProductId, Review = product.Review }).ToList();
            foreach (var element in p)
            {
                Console.WriteLine("ProductId : " + element.ProductId + " \t " + "Review" + element.Review);
                Console.WriteLine("-----------------------");
            }
        }
        //UC-6
        //This method for skip top 5 rcords and retrive other data
        public static void SkipTop5RecordsFromListAndRetrieveOtherData(List<ProductReview> list)
        {
            //Query syntax for LINQ 
            var result = (from product in list orderby product.Rating descending select product).Skip(5);
            var remainingRecords = result;
            foreach (ProductReview product in remainingRecords)
            {
                Console.WriteLine("ProductId : " + product.ProductId + " UserId : " + product.UserId + " Rating : " + product.Rating + " Review : " + product.Review + " IsLike : " + product.IsLike);
            }
        }
        //UC-7
        //This method for Retrieve product id and review from list of all recoprds
        public static void RetrieveProductIDAndReview1(List<ProductReview> productReviewsList)
        {
            var p = productReviewsList.Select(product => new { productID = product.ProductId, review = product.Review });
            foreach (var element in p)
            {
                Console.WriteLine("ProductID: " + element.productID + "\t" + "Review:" + element.review);
            }
        }
        //UC-8
        //This method for create data table 
        public static void CreateDataTable()
        {
            DataTable table = new DataTable();
            table.Columns.Add("ProductID", typeof(int));
            table.Columns.Add("UserID", typeof(int));
            table.Columns.Add("Rating", typeof(int));
            table.Columns.Add("Review", typeof(string));
            table.Columns.Add("IsLike", typeof(bool));

            table.Rows.Add(1, 1, 17, "good", true);
            table.Rows.Add(2, 3, 1, "bad", false);
            table.Rows.Add(3, 5, 20, "good", true);
            table.Rows.Add(4, 7, 10, "average", true);
            table.Rows.Add(5, 1, 5, "bad", false);
            table.Rows.Add(6, 5, 30, "good", true);
            table.Rows.Add(7, 7, 13, "average", true);
            table.Rows.Add(8, 1, 2, "bad", false);

            Console.WriteLine("Retrieved data are: ");
            //RetrieveDataFromDataTable(table);
            Console.WriteLine();
            RetrieveDataFromDataTablesWhoseIsLikeValueIsTrue(table);
            Console.WriteLine();
            RetrieveDataFromDataTablesWhoseReviewValueIsGood(table);


        }
        public static void RetrieveDataFromDataTable(DataTable table)
        {
            var result = (from product in table.AsEnumerable() select product.Field<int>("ProductID")).ToList();
            Console.WriteLine("Product ID's are");
            foreach (var product in result)
            {
                Console.WriteLine(product);
            }
        }
        //This method for retrieve records who's Islike value is true
        public static void RetrieveDataFromDataTablesWhoseIsLikeValueIsTrue(DataTable table)
        {
            var result = (from product in table.AsEnumerable() where product.Field<bool>("IsLike") == true select product.Field<int>("ProductID")).ToList();
            Console.WriteLine("Product Id of Who's Islike value is true are : ");
            foreach (var product in result)
            {
                Console.WriteLine("Product ID : " + product);
            }
        }

        //This method for retrive product id whose review is good
        public static void RetrieveDataFromDataTablesWhoseReviewValueIsGood(DataTable table)
        {
            var result = (from product in table.AsEnumerable() where product.Field<string>("Review") == "good" select product.Field<int>("ProductID")).ToList();
            Console.WriteLine("Product Id of Whose review is good : ");
            foreach (var product in result)
            {
                Console.WriteLine("Product ID : " + product);
            }
        }
    }
}

