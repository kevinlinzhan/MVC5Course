namespace MVC5Course.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    [MetadataType(typeof(ProductMetaData))]
    public partial class Product : IValidatableObject
    {
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if(Price > 1000 && Stock > 100)
            {
                yield return new ValidationResult("價格大於1000的商品庫存不能超過1000", new string[] { "Price", "Stock" });
            }

            //if (ProductName.Contains("kevin"))
            //{
            //    yield return new ValidationResult("注冊商標不能使用此名稱", new string[] { "ProductName" });
            //}
        }
    }

    public partial class ProductMetaData
    {
        [Required]
        public int ProductId { get; set; }
        
        [StringLength(80, ErrorMessage="欄位長度不得大於 80 個字元")]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    
        public virtual ICollection<OrderLine> OrderLine { get; set; }
    }
}
