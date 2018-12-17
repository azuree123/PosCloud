using System;
using System.ComponentModel.DataAnnotations.Schema;

using POSApp.Core.Shared;

namespace POSApp.Core.Domain
{
    public class CartItem : IStateObject, ICartItem
    {
        private CartItem(int productId, int quantity, decimal displayedPrice, string cartCookie)
        {
            CartCookie = cartCookie;
            ProductId = productId;
            CurrentPrice = displayedPrice;
            Quantity = quantity;
            SelectedDateTime = DateTime.UtcNow;
            State = ObjectState.Added;
        }

        private CartItem(int productId, int quantity, decimal displayedPrice, int cartId)
        {
            ProductId = productId;
            Quantity = quantity;
            CurrentPrice = displayedPrice;
            CartId = cartId;
            SelectedDateTime = DateTime.UtcNow;
            State = ObjectState.Added;
        }

        private CartItem()
        {
        }

        internal static CartItem Create(int productId, int quantity, decimal displayedPrice, string cartCookie)
        {
            return new CartItem(productId, quantity, displayedPrice, cartCookie);
        }

        public static CartItem Create(int productId, int quantity, decimal displayedPrice, int cartId)
        {
            return new CartItem(productId, quantity, displayedPrice, cartId);
        }

        public int CartItemId { get; private set; }
        public string CartCookie { get; private set; }


        public int CartId { get; set; }

        [ForeignKey("CartId")]
        public NewCart Cart { get; set; }

        public int ProductId { get; private set; }
        public Product Product { get; set; }

        public DateTime SelectedDateTime { get; private set; }
        public decimal CurrentPrice { get; private set; }
        public int Quantity { get; private set; }
        public ObjectState State { get; private set; }

        public void UpdateQuantity(int newQuantity)
        {
            if (Quantity != newQuantity)
            {
                Quantity = newQuantity;
                State = ObjectState.Modified;
            }
        }
    }
}