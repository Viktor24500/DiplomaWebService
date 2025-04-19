function closeModalForm() {
	let form = document.getElementsByClassName("modal")[0];

	if (form) {
		form.reset();
	}
	document.getElementsByClassName("modal")[0].style.display = "none";
}
function openModalForm() {
	document.getElementsByClassName("modal")[0].style.display = "block";
}
function openLogOutForm() {
	document.getElementById("logout").style.display = "block";
}
function openMenu(id) {
	//document.getElementById("modal-popup-hidden"+id).style.display = "block";
	document.getElementsByClassName("modal-popup-container")[id].style.display = "block";
}
function openFilter(id) {
	document.getElementById("modal-popup-hidden-filters")[id].style.display = "block";
}