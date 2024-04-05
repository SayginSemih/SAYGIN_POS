const input = document.querySelector("#not");
const txtNot = document.querySelector(".not");

txtNot.addEventListener("change", () => {
    input.value=txtNot.value;
})