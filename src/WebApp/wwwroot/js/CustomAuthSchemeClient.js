import { getWithHeader, displayResponse } from "/js/ApiCalls.js";

async function makeFetch() {
    const headerName = document.getElementById("header-name").value;
    const headerVal = document.getElementById("header-val").value;

    const response = await getWithHeader("/CustomAuthScheme/GetData", headerName, headerVal);

    await displayResponse(response);
}

document.querySelector('#make-fetch').addEventListener('click', makeFetch);