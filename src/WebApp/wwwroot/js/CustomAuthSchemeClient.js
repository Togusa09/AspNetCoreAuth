import { getCustomAuthData } from "/js/ApiCalls.js";

async function makeFetch() {
    const headerName = document.getElementById("header-name").value;
    const headerVal = document.getElementById("header-val").value;

    const response = await getCustomAuthData(headerName, headerVal);

    console.log(response.status);
    document.getElementById("response-status").innerHTML = response.status;
    // To extract data, use await response.json()
    document.getElementById("response-content").innerHTML = await response.text();
}

document.querySelector('#make-fetch').addEventListener('click', makeFetch);