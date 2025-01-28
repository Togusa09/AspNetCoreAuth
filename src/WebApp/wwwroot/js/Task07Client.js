import { get, getWithHeader, displayResponse } from "/js/ApiCalls.js";

async function getToken() {
    var response = await get("Task07/GetUserJwt");
    const responseContent = await displayResponse(response);
    document.getElementById("token-val").value = responseContent.trim('"');
}

async function makeFetch() {
    const headerVal = document.getElementById("token-val").value;

    const response = await getWithHeader("Task07/TestJwt", "Authorization", `Bearer ${headerVal}`);

    await displayResponse(response);
}

document.querySelector('#make-fetch').addEventListener('click', makeFetch);
document.querySelector('#get-token').addEventListener('click', getToken);