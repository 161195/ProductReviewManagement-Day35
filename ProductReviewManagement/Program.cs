using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

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
                new ProductReview(){ ProductId=5,UserId=1,Review="bad",Rating=5,IsLike=false}
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
            var p = list.Select(product => new { ProductId = product.ProductId, Review = product.Review}).ToList();
            foreach (var element in p)
            {
                Console.WriteLine("ProductId : " + element.ProductId + " \t " + "Review" + element.Review);
                Console.WriteLine("-----------------------");
            }
        }
    } 
}
