document.querySelectorAll('.sector-checkbox').forEach(cb => {
    cb.addEventListener('change', () => {
        const selected = Array.from(document.querySelectorAll('.sector-checkbox:checked'))
            .map(cb => cb.value);
       
        let Http = new XMLHttpRequest();
        let urlWithParameters='';
        for (let i = 0; i < selected.length; i++)
        {
            urlWithParameters = urlWithParameters + "sectors=" + selected[i] + "&";
        }
        let url = `/filterItems/?` + urlWithParameters;
        url = url.substring(0, url.length - 1);
        Http.open("GET", url);
        Http.setRequestHeader("Content-Type", "application/json; charset=utf-8")
        Http.send();

        Http.onreadystatechange = function () {
            if (Http.readyState === 4 && Http.status === 200) {
                console.log(Http.responseText);
                if (Http.status === 200) {

                }
            }
        };
    });
});
