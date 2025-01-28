import { getWithHeader, displayResponse } from "/js/ApiCalls.js";

async function makeFetch() {
    const headerName = document.getElementById("header-name").value;
    const headerVal = document.getElementById("header-val").value;

    const response = await getWithHeader("Task05/GetData", headerName, headerVal);

    await displayResponse(response);
}

document.querySelector('#make-fetch').addEventListener('click', makeFetch);