const url = "https://localhost:5001/api/beanvariety/";

const button = document.querySelector("#run-button");
const beanList = document.querySelector("#beanList");

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