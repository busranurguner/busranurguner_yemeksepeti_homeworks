const taskList = [
  "Daha Guzel Tasarim",
  "Todo lar calisir hale getirilicek",
  "herhagi bir task",
];

// create element - ul appendChild yap ++
// kopyala yapıştır yaptık, fonksiyonla yapsak daha iyiydi++
// kod çirkin duruyor, kod okunaklı güzel görünsün. ++
// javascript kodunu ayır  +++
// querySelector kullanalım (getElementById yerine)++

// Özgür -  Ahmet Suhan için ->  css'lleri obje yap  -> en son "li" elementi renklendir.
// Taskları localStorage'e kaydet ve sayfa yenilendiğinde oradan al

var mylist = document.getElementsByTagName("LI");
var i;
for (i = 0; i < mylist.length; i++) {
  var span = document.createElement("SPAN");  
  var txt = document.createTextNode("\u00D7"); 
  span.className = "close";
  span.appendChild(txt);
  mylist[i].appendChild(span);
}


//Delete
var close = document.getElementsByClassName("close");
var i;
for (i = 0; i < close.length; i++) {
  close[i].onclick = function() {
    var div = this.parentElement;
    div.style.display = "none";
  }
}


// Create a new list item when clicking on the "Add" button
function newElement() {
  var li = document.createElement("li");
  var inputValue =document.querySelector('.input-todo').value;
  var t = document.createTextNode(inputValue);
  li.appendChild(t);
  document.getElementById("ul-list").appendChild(li);
  document.getElementById("text-input").value = "";

  var span = document.createElement("SPAN");
  var txt = document.createTextNode("\u00D7");
  span.className = "close";
  span.appendChild(txt);
  li.appendChild(span);

  for (i = 0; i < close.length; i++) {
    close[i].onclick = function() {
      var div = this.parentElement;
      div.style.display = "none";
    }
  }
}





     /* let listElements = "";
      taskList.map((item, index) => {
        console.log(item);
        const myTask = `
        <li id="li_${index}">
             ${item} 
            <input id="btn_sil_${index}"
             type="button" 
             value="Sil"> 
          </li>`;
        listElements += myTask;
      });
      ulList.innerHTML = listElements;

      taskList.map((item, index) => {
        document
          .getElementById("btn_sil_" + index)
          .addEventListener("click", function () {
            console.log(item);
          });
      });

      button.addEventListener("click", function (e) {
        taskList.push(textInput.value);

        let listElements = "";
        taskList.map((item) => {
          console.log(item);
          const myTask = `<li>${item}</li>`;
          listElements += myTask;
        });
        ulList.innerHTML = listElements;
      });*/



