function ReassessmentWithCoeffClose() {
	let form = document.getElementById("reassessmentWithCoefForm");

	if (form) {
		form.reset();
	}
	document.getElementById('reassessmentWithCoefForm').style.display = "none";
}
function ReassessmentWithoutCoeffOpen(stockId, displayText) {
	document.getElementById('showStockItemId').value = stockId;
	document.getElementById('displayText').value = displayText;
	document.getElementById('reassessmentWithoutCoefForm').style.display = "block";
}
function ReassessmentWithoutCoeffClose() {
	let form = document.getElementById("reassessmentWithoutCoefForm");

	if (form) {
		form.reset();
	}

	document.getElementById('reassessmentWithoutCoefForm').style.display = "none";
}
function ReassessmentWithCoeffOpen() {
	document.getElementById("reassessmentWithCoefForm").style.display = "block";
}