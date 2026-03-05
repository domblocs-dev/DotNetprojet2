using System.Collections.Generic;
using System.Linq;

namespace P2FixAnAppDotNetCode.Models
{
    /// <summary>
    /// The Cart class
    /// </summary>
    public class Cart : ICart
    {
        /// <summary>
        /// Read-only property for display only
        /// </summary>
        public IEnumerable<CartLine> Lines => _cartLines;

        /// <summary>
        /// Return the actual cartline list
        /// </summary>
        private readonly List<CartLine> _cartLines = new();

        /// <summary>
        /// Adds a product in the cart or increment its quantity in the cart if already added
        /// </summary>//
        public void AddItem(Product product, int quantity)
        {
            //4 TODO implement the method
            if (product == null || quantity <= 0)
            {
                return;
            }

            // Si le produit existe déjà dans le panier, incrémenter la quantité
            var existing = _cartLines.FirstOrDefault(l => l.Product?.Id == product.Id);
            if (existing != null)
            {
                existing.Quantity += quantity;
                return;
            }
            _cartLines.Add(new CartLine { Product = product, Quantity = quantity });
        }

        /// <summary>
        /// Removes a product form the cart
        /// </summary>
        public void RemoveLine(Product product) =>
            _cartLines.RemoveAll(l => l.Product != null && l.Product.Id == product.Id);

        /// <summary>
        /// Get total value of a cart
        /// </summary>
        public double GetTotalValue()
        {
            //5 TODO implement the method
            double total = 0.0;
            foreach (var line in _cartLines)
            {
                if (line?.Product == null) continue;
                total += line.Product.Price * line.Quantity;
            }
            return total;
        }

        /// <summary>
        /// Get average value of a cart
        /// </summary>
        public double GetAverageValue()
        {
            //6 TODO implement the method
            int totalQuantity = _cartLines.Sum(l => l.Quantity);
            double totalValue = 0.0;
            foreach (var line in _cartLines)
            {
                if (line?.Product == null) continue;
                totalValue += line.Product.Price * line.Quantity;
            }
            if (totalQuantity > 0)
            {
                return totalValue / totalQuantity;
            }
            return 0.0;
        }

        /// <summary>
        /// Looks after a given product in the cart and returns if it finds it
        /// </summary>
        public Product FindProductInCartLines(int productId)
        {
            //7 TODO implement the method
            CartLine cartLine = _cartLines.FirstOrDefault(l => l.Product != null && l.Product.Id == productId);
            if (cartLine != null)
            {
                return cartLine.Product;
            }
            return null;
        }

        /// <summary>
        /// Get a specific cartline by its index
        /// </summary>
        public CartLine GetCartLineByIndex(int index)
        {
            return Lines.ToArray()[index];
        }

        /// <summary>
        /// Clears a the cart of all added products
        /// </summary>
        public void Clear()
        {
            List<CartLine> cartLines = _cartLines;
            cartLines.Clear();
        }
    }

    public class CartLine
    {
        public int OrderLineId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
