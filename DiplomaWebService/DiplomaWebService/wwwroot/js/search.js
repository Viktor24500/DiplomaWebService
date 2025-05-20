function searchContragents(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchContragents/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send(); // No data needed for GET request

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("contragentsList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

function searchItems(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchItems/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send(); // No data needed for GET request

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("itemsList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

function searchInvoiceIn(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchInvoicesIn/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send();

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("invoiceInList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

function searchInvoiceOut(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchInvoicesOut/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send();

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("invoiceOutList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

function searchUnits(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchUnits/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send(); // No data needed for GET request

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("unitsList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

function searchUsers(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchUsers/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send(); // No data needed for GET request

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("usersList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

function searchStockItem(event) {
	if (event.key === 'Enter') {
		let searchValue = document.getElementById("search").value;
		let Http = new XMLHttpRequest();
		let url = "/searchStockItems/" + encodeURIComponent(searchValue);
		Http.open("GET", url);
		Http.send(); // No data needed for GET request

		Http.onreadystatechange = function () {
			if (Http.readyState === 4) {
				console.log(Http.responseText);
				if (Http.status === 200) {
					document.getElementById("usersList").innerHTML = Http.responseText;
				}
				else {
					document.getElementsByClassName("sidebar-page")[0].innerHTML = Http.responseText;
				}
			}
		};
	}
}

