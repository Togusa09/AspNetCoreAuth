
async function get(path) {
    const request = new Request(path, {
        method: "GET",
        redirect: "error"
    });
    return await fetch(request);
}

async function displayResponse(response) {
    console.log(response.status);
    document.getElementById("response-status").innerHTML = response.status;
    // To extract data, use await response.json()
    document.getElementById("response-content").innerHTML = await response.text();
}

async function getNoAuth() {
    const response = await get(`Claims/NoAuth`);
    displayResponse(response);
}

async function getAuth() {
    const response = await get(`Claims/Auth`);
    displayResponse(response);
}

async function getPilot() {
    const response = await get(`Claims/IsInRole/Pilot`);
    displayResponse(response);
}

async function getEngineer() {
    const response = await get(`Claims/IsInRole/Engineer`);
    displayResponse(response);
}

document.querySelector('#get-no-auth').addEventListener('click', getNoAuth);
document.querySelector('#get-auth').addEventListener('click', getAuth);
document.querySelector('#get-pilot').addEventListener('click', getPilot);
document.querySelector('#get-engineer').addEventListener('click', getEngineer);