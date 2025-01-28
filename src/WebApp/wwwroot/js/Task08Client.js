import { get, displayResponse } from "/js/ApiCalls.js";

export function getCraftDetailApi(craft) {
    return get(`Task08/Craft/${craft}`);
}

async function getCraft() {
    const response = await get(`Task08/Craft`); 

    const selectTag = document.getElementById("craft-list")
    selectTag.innerHTML = null;

    const returnedCraft = await displayResponse(response);

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
    await displayResponse(response);
}

document.querySelector('#get-craft').addEventListener('click', getCraft);
document.querySelector('#get-craft-detail').addEventListener('click', getCraftDetail);