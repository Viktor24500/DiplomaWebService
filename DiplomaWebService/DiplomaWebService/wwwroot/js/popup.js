document.addEventListener("DOMContentLoaded", function () {
    const elementsToHide = [
        "logout",
        "modal-popup-hidden",
        "modal-popup-hidden-filters"
    ];

    const elements = elementsToHide.map(id => document.getElementById(id)).filter(el => el !== null);

    document.addEventListener('click', function (e) {
        elements.forEach(el => {
            if (el.style.display === "block" && !el.contains(e.target)) {
                el.style.display = "none";
            }
        });
    });
});
