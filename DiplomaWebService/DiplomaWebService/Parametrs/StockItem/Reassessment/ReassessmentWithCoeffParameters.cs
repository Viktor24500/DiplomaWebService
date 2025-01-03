namespace DiplomaWebService.Parametrs.StockItem.Reassessment
{
	public class ReassessmentWithCoeffParameters
	{
		public ReassessmentWithCoeffParameters(decimal oldPrice, decimal coeff)
		{
			OldPrice = oldPrice;
			Coeff = coeff;
		}

		public decimal OldPrice { get; set; }
		public decimal Coeff { get; set; }
	}
}
