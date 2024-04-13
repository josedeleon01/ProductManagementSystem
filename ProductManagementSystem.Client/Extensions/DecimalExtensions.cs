namespace ProductManagementSystem.Client
{
    public static class DecimalExtensions
    {
        public static string ToFormattedPrice(this decimal price)
        {
            return String.Format("{0:C2}", price); 
		}
    }
}
