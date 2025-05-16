function SendInvoice() {
	debugger;
	let invoiceId = document.getElementById("invoiceId").value;
	let invoiceDate = document.getElementById("invoiceDate").value;
	let invoiceNumber = document.getElementById("invoiceNumber").value;
	let destinationId = document.getElementById("destinationIdInput").value;
	let senderId = document.getElementById("senderId").value;
	let sectorId = document.getElementById("sectorId").value;
	let documentTypeId = document.getElementById("documentTypeId").value;

	let positions = [];
	let table = document.getElementById("inoicePositionsTable"); // Replace with your table's actual ID
	let rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");

	for (let row of rows) {
		let cells = row.getElementsByTagName("td");

		let position = {
			ItemId: parseInt(cells[0].textContent, 10), // Hidden column
			SerialNumber: cells[2].textContent.trim(),
			ProductionYear: parseInt(cells[3].textContent, 10),
			Amount: parseFloat(cells[4].textContent.replace(",", ".")),
			Price: parseFloat(cells[5].textContent.replace(",", ".")),
			CategoryId: parseInt(cells[8].textContent, 10) // Hidden column
		};

		positions.push(position);
	}

	let Http = new XMLHttpRequest();
	let url = "/invoiceIn/";
	Http.open("POST", url);
	Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
	Http.send(
		JSON.stringify(
			{
				InvoiceDate: invoiceDate,
				Number: invoiceNumber,
				DestinationId: parseInt(destinationId, 10),
				SenderId: parseInt(senderId, 10),
				SectorId: parseInt(sectorId, 10),
				DocumentTypeId: parseInt(documentTypeId, 10),
				Positions: positions 
			},
		)
	);

	Http.onreadystatechange = function () {
		if (Http.readyState === 4 && Http.status === 200) {
			console.log(Http.responseText);
		}
	};
}