function SendInvoiceIn() {
	debugger;
	let invoiceId = document.getElementById("invoiceId").value;
	let invoiceDate = document.getElementById("invoiceDate").value;
	let invoiceNumber = document.getElementById("invoiceNumber").value;
	let destinationId = document.getElementById("destinationIdHidden").value;
	let senderId = document.getElementById("senderId").value;
	let sectorId = document.getElementById("sectorId").value;
	let documentTypeId = document.getElementById("documentTypeId").value;

	let positions = [];
	let table = document.getElementById("invoiceInPositionsTable");
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

	let data = {
		InvoiceDate: invoiceDate,
		Number: invoiceNumber,
		DestinationId: parseInt(destinationId, 10),
		SenderId: parseInt(senderId, 10),
		SectorId: parseInt(sectorId, 10),
		DocumentTypeId: parseInt(documentTypeId, 10),
		Positions: positions
	}
	let Http = new XMLHttpRequest();
	let url = "/invoiceIn/";
	Http.open("POST", url);
	Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
	Http.send(
		JSON.stringify(data)
	);

	Http.onreadystatechange = function () {
		if (Http.readyState === 4) {
			console.log(Http.responseText);
			if (Http.status === 200) {
				window.location.href = '/invoicesIn';
			}
			else {
				document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
			}
		}
	};
}
function GetItemBySectorId() {
	debugger;
	let sectorId = document.getElementById("sectorId").value;
	let Http = new XMLHttpRequest();
	let url = "/itemsBySectorId/" + encodeURIComponent(sectorId);
	Http.open("GET", url);
	Http.send(); // No data needed for GET request

	Http.onreadystatechange = function () {
		if (Http.readyState === 4) {
			console.log(Http.responseText);
			if (Http.status === 200) {
				document.getElementById("inoiceInPositionsTableItemsList").innerHTML = Http.responseText;
			}
			else {
				document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
			}
		}
	};
}

var invoiceOutRowCounter = 0;
var invoiceInRowCounter = 0;

function addPositionIn() {
	let amount = document.getElementById("amount").value;
	let price = document.getElementById("price").value;
	let serialNumber = document.getElementById("serialNumber").value;
	let productionYear = document.getElementById("productionYear").value;
	let rows = document.querySelectorAll('#invoiceInPositionsTableItems tbody tr');

	//get category id and name
	//let categoryId = document.getElementById("categoryId").value;
	//let categoryName = document.getElementById("categoryId").options[document.getElementById("categoryId").selectedIndex].text;

	rows.forEach(row => {
		let checkbox = row.querySelector('.invoice-position-item-checkbox');
		if (checkbox && checkbox.checked) {
			let itemId = row.querySelector('.invoice-position-table-items-item-id').textContent.trim();
			let itemName = row.querySelector('.invoice-position-table-items-item-name').textContent.trim();
			let unitId = row.querySelector('.invoice-position-table-items-unit-id').textContent.trim();
			let unitName = row.querySelector('.invoice-position-table-items-unit-name').textContent.trim();

			// Assume categoryId/categoryName are optional or static for now
			let categoryId = document.getElementById("categoryId").value; // or from a hidden field/input
			let categoryName = document.getElementById("categoryId").options[document.getElementById("categoryId").selectedIndex].text;

			let targetTable = document.getElementById("invoiceInPositionsTable").getElementsByTagName("tbody")[0];
			let newRow = targetTable.insertRow();
			newRow.classList.add("data-table-tr");

			newRow.innerHTML = `
                    <td class="data-table-td" hidden>${itemId}</td>
                    <td class="data-table-td">${itemName}</td>
                    <td class="data-table-td">${serialNumber}</td>
                    <td class="data-table-td">${productionYear}</td>
                    <td class="data-table-td">${amount}</td>
                    <td class="data-table-td">${price}</td>
                    <td class="data-table-td" hidden>${unitId}</td>
                    <td class="data-table-td">${unitName}</td>
                    <td class="data-table-td" hidden>${categoryId}</td>
                    <td class="data-table-td">${categoryName}</td>
                `;

			checkbox.checked = false;
		}
	});
}