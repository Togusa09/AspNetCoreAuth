import { getCraftDetailApi, getCraftAtLaunchsite } from "/js/ApiCalls.js";

function setDisplay(status, data) {
    console.log(status);
    document.getElementById("response-status").innerHTML = status;
    document.getElementById("response-content").innerHTML = JSON.stringify(data);
}

async function getCraft() {
    const launchSite = document.getElementById("launch-site").value;
    const response = await getCraftAtLaunchsite(launchSite);

    const selectTag = document.getElementById("craft-list")
    selectTag.innerHTML = null;

    const returnedCraft = await response.json();
    setDisplay(response.status, returnedCraft);

    if (response.ok) {
        returnedCraft.map((craft, i) => {
            let opt = document.createElement("option");
            opt.value = craft.name; // the index
            opt.innerHTML = craft.name;
            selectTag.append(opt);
        });
    }
    else {
        let opt = document.createElement("option");
        opt.innerHTML = "N/A";
        selectTag.append(opt);
    }
}

async function getCraftDetail() {
    const craft = document.getElementById("craft-list").value;
    const response = await getCraftDetailApi(craft);

    const returnedCraft = await response.json();

    setDisplay(response.status, returnedCraft);
}

document.querySelector('#get-craft').addEventListener('click', getCraft);
document.querySelector('#get-craft-detail').addEventListener('click', getCraftDetail);