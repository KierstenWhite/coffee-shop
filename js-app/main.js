import { AddABeanForm } from "./AddABeanForm.js";

const url = "https://localhost:5001/api/beanvariety/";

const button = document.querySelector("#run-button");
const beanList = document.querySelector("#beanList");
const beanForm = document.querySelector("#addABeanForm");
beanForm.innerHTML = AddABeanForm()

button.addEventListener("click", () => {
    getAllBeanVarieties()
        .then(beanVarieties => {
            // console.log(beanVarieties);
            let html = "<ul>"

            beanVarieties.forEach(variety => {
                html += `<li value=${variety.Id}>${variety.name}: From ${variety.region} | ${variety.notes}</li>`
            });

            html += "</ul>"
            beanList.innerHTML = html
        })
});

function getAllBeanVarieties() {
    return fetch(url).then(resp => resp.json());
}

export const sendRequest = (newBean) => {
    const fetchOptions = {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(newBean)
    }

    return fetch(url, fetchOptions)
        .then(response => response.json())
        .then(() => {
            beanForm.dispatchEvent(new CustomEvent("stateChanged"))
        })
        
}
