namespace DiplomaWebService.Parametrs.StockItem.Reassessment
{
	public class ReassessmentWithoutCoeffParameters
	{
		public ReassessmentWithoutCoeffParameters(decimal oldPrice, decimal newPrice)
		{
			OldPrice = oldPrice;
			NewPrice = newPrice;
		}

		public decimal OldPrice { get; set; }
		public decimal NewPrice { get; set; }
	}
}
