import { sendRequest } from "./main.js"

const beanForm = document.querySelector("#addABeanForm")

export const AddABeanForm = () => {
    return `
        <div class="mb-3">
            <label for="name" class="form-label">Bean Variety Name</label>
            <input type="text" name="bname" class="form-control">
        </div>
        <div class="mb-3">
            <label for="region" class="form-label">Region</label>
            <input type="text" name="region" class="form-control">
        </div>
        <div class="mb-3">
            <label for="notes" class="form-label">Notes</label>
            <input type="text" name="notes" class="form-control">
        </div>

        <button class="button" id="addBeanButton">Add Bean Variety</button>
    `
}



// listens for button click on form, takes the data, and sends a request to store in the API
beanForm.addEventListener("click", clickEvent => {
    if (clickEvent.target.id === "addBeanButton") {
        
        const bname = document.querySelector("input[name='bname']").value
        const region = document.querySelector("input[name='region']").value
        const notes = document.querySelector("input[name='notes']").value

        const dataToSendToAPI = {
            Name: bname,
            Region: region,
            Notes: notes
        }

        sendRequest(dataToSendToAPI)

    }
})