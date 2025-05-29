function SendInvoiceOut() {
	debugger;
	let invoiceId = document.getElementById("invoiceId").value;
	let invoiceDate = document.getElementById("invoiceDate").value;
	let invoiceNumber = document.getElementById("invoiceNumber").value;
	let destinationId = document.getElementById("destinationId").value;
	let senderId = document.getElementById("senderIdHidden").value;
	let sectorId = document.getElementById("sectorId").value;
	let documentTypeId = document.getElementById("documentTypeId").value;

	let positions = [];
	let table = document.getElementById("invoiceOutPositionsTable");
	let rows = table.getElementsByTagName("tbody")[0].getElementsByTagName("tr");

	for (let row of rows) {
		let cells = row.getElementsByTagName("td");

		let position = {
			StockItemId: parseInt(cells[0].textContent, 10), // Hidden column
			Amount: parseFloat(cells[5].textContent.replace(",", ".")),
			//ItemId: parseInt(cells[1].textContent, 10), // Hidden column
			//SerialNumber: cells[3].textContent.trim(),
			//ProductionYear: parseInt(cells[4].textContent, 10),
			//Amount: parseFloat(cells[5].textContent.replace(",", ".")),
			//Price: parseFloat(cells[6].textContent.replace(",", ".")),
			//CategoryId: parseInt(cells[9].textContent, 10) // Hidden column
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
	let url = "/invoiceOut/";
	Http.open("POST", url);
	Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
	Http.send(
		JSON.stringify(data)
	);

	Http.onreadystatechange = function () {
		if (Http.readyState === 4) {
			console.log(Http.responseText);
			if (Http.status === 200) {
				window.location.href = '/invoicesOut';
			}
			else {
				document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
			}
		}
	};
}

function GetStockItemBySenderAndSector()
{
	let sectorId = document.getElementById("sectorId").value;
	let senderId = document.getElementById("senderId").value;
	let Http = new XMLHttpRequest();
	let url = "/stockItemsByContragentIdAndSectorId/?sectorId=" + sectorId + "&contragentId=" + senderId;
	Http.open("GET", url);
	Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
	Http.send();

	Http.onreadystatechange = function () {
		if (Http.readyState === 4) {
			console.log(Http.responseText);
			if (Http.status === 200) {
				document.getElementById("inoiceOutPositionsTableStockItemItemsList").innerHTML = Http.responseText;
			}
			else {
				document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
			}
		}
	};
}

function addPositionOut()
{
	//let stockItemId = document.getElementById("stockItemId").value;
	let amount = document.getElementById("amount").value;
	let rows = document.querySelectorAll('#invoiceOutPositionsTableStockItem tbody tr');

	//get category id and name
	//let categoryId = document.getElementById("categoryId").value;
	//let categoryName = document.getElementById("categoryId").options[document.getElementById("categoryId").selectedIndex].text;

	rows.forEach(row => {
		let checkbox = row.querySelector('.invoice-position-stockitem-checkbox');
		if (checkbox && checkbox.checked) {
			let stockitemId = row.querySelector('.invoice-position-table-stockitems-stockitem-id').textContent.trim();
			let itemId = row.querySelector('.invoice-position-table-stockitems-item-id').textContent.trim();
			let itemName = row.querySelector('.invoice-position-table-stockitems-item-name').textContent.trim();
			let serialNumber = row.querySelector('.invoice-position-table-stockitems-serial-number').textContent.trim();
			let productionYear = row.querySelector('.invoice-position-table-stockitems-production-year').textContent.trim();
			let price = row.querySelector('.invoice-position-table-stockitems-price').textContent.trim();
			let unitId = row.querySelector('.invoice-position-table-stockitems-unit-id').textContent.trim();
			let unitName = row.querySelector('.invoice-position-table-stockitems-unit-name').textContent.trim();
			let categoryId = document.querySelector(".invoice-position-table-stockitems-category-id").textContent.trim();
			let categoryName = document.querySelector(".invoice-position-table-stockitems-category-name").textContent.trim();
			

			let targetTable = document.getElementById("invoiceOutPositionsTable").getElementsByTagName("tbody")[0];
			let newRow = targetTable.insertRow();
			newRow.classList.add("data-table-tr");

			newRow.innerHTML = `
					<td class="data-table-td" hidden>${stockitemId}</td>
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