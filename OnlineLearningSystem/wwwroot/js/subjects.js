function displayList() {
    var listMode = document.getElementById('list-mode');
    var gridMode = document.getElementById('grid-mode');
    listMode.style.display = 'block';
    //gridMode.style.display = 'none !important';
    gridMode.style.setProperty('display', 'none', 'important');
}
function displayCard() {
    var listMode = document.getElementById('list-mode');
    var gridMode = document.getElementById('grid-mode');
    listMode.style.display = 'none';
    gridMode.style.display = 'block';
}