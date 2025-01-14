import { getCraftAtLaunchsite } from "/js/ApiCalls.js";

async function getCraft() {
    const launchSite = document.getElementById("launch-site").value;
    const response = await getCraftAtLaunchsite(launchSite);

    document.getElementById("response-status").innerHTML = response.status;
    document.getElementById("response-content").innerHTML = await response.text();
}

document.querySelector('#get-craft').addEventListener('click', getCraft);