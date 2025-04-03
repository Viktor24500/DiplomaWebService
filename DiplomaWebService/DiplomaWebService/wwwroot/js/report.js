function createReportInvoiceOut(invoiceId) {
	let Http = new XMLHttpRequest();
	let url = "/reportInvoiceOut/";
	Http.open("POST", url);
	Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
	Http.send(JSON.stringify({ invoiceId: invoiceId }));

	Http.onreadystatechange = function () {
		if (Http.readyState === 4 && Http.status === 200) {
			console.log(Http.responseText);
		}
	};
}

function createReportInvoiceIn(invoiceId) {
	let Http = new XMLHttpRequest();
	let url = "/reportInvoiceIn/";
	Http.open("POST", url);
	Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
	Http.send(JSON.stringify({ invoiceId: invoiceId }));

	Http.onreadystatechange = function () {
		if (Http.readyState === 4 && Http.status === 200) {
			console.log(Http.responseText);
		}
	};
}