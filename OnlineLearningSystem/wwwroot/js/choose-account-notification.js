function chooseAll() {
    var item = document.getElementById("all");
    if (item.checked) {
        var listclassroom = document.getElementsByClassName("classroom");
        for (var i = 0; i < listclassroom.length; i++) {
            listclassroom[i].checked = true;
        }
        var listcbx = document.getElementsByClassName("student");
        for (var i = 0; i < listcbx.length; i++) {
            listcbx[i].checked = true;
        }
    } else {
        var listclassroom = document.getElementsByClassName("classroom");
        for (var i = 0; i < listclassroom.length; i++) {
            listclassroom[i].checked = false;
        }
        var listcbx = document.getElementsByClassName("student");
        for (var i = 0; i < listcbx.length; i++) {
            listcbx[i].checked = false;
        }
    }
}
function chooseAllIn(no) {
    console.log(no);
    var item = document.getElementById("class-"+no);
    if (item.checked) {
        var listStudent = document.getElementsByClassName(no);
        for (var i = 0; i < listStudent.length; i++) {
            listStudent[i].checked = true;
        }
    } else {
        var listStudent = document.getElementsByClassName(no);
        for (var i = 0; i < listStudent.length; i++) {
            listStudent[i].checked = false;
        }
    }
}
function change(id) {
    var item = document.getElementById(id);
    if (item.checked) {
        var parent = document.getElementById("class" + item.className);
    }
}