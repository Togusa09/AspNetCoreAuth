async function makeFetch() {
    const headerName = document.getElementById("header-name").value;
    const headerval = document.getElementById("header-val").value;

    var headers = {};
    headers[headerName] = headerval;

    const request1 = new Request("/CustomAuthScheme/GetData", {
        method: "GET",
        headers: headers
    });

    const response = await fetch(request1);
    console.log(response.status);
    document.getElementById("response-status").innerHTML = response.status;
    // To extract data, use await response.json()
    document.getElementById("response-content").innerHTML = await response.text();
}

document.querySelector('#make-fetch').addEventListener('click', makeFetch);