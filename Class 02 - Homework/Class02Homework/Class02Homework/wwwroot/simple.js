let getAllBtn = document.getElementById("btn1");
let getByIdBtn = document.getElementById("btn2");
let addUserBtn = document.getElementById("btn3");
let getAllTagsBtn = document.getElementById("btn4");
let getTagByIdBtn = document.getElementById("btn5");
let getByIdInput = document.getElementById("input2");
let addUserInput = document.getElementById("input3");

//change the port from your api
let port = "5062";
let getAllUsers = async () => {
    let url = "http://localhost:" + port + "/api/users";

    let response = await fetch(url);
    console.log(response);
    debugger;
    let data = await response.json();
    console.log(data);
};

let getUserById = async () => {
    let url = "http://localhost:" + port + "/api/users/" + getByIdInput.value;
    debugger
    let response = await fetch(url);
    let data = await response.text();
    console.log(data);
};


let addUser = async () => {
    let url = "http://localhost:" + port + "/api/users";
   var response = await fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'text/plain'
        },
       body: addUserInput.value 
   });
   var data = await response.text();
   console.log(data);
}


getAllBtn.addEventListener("click", getAllUsers);
getByIdBtn.addEventListener("click", getUserById);
addUserBtn.addEventListener("click", addUser);