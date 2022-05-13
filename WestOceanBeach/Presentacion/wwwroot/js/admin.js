





function updateAboutUs() {
   

    var text = document.getElementById('aboutUs') ;

    if (trimfield(text.value) == '') {
      
        var aswer = document.getElementById('answer');
        aswer.innerHTML = "Complete el campo no puede ir vacio";
        var modal = document.getElementById("myModal");
        modal.style.display = "block";
        
    } else {






    }



}

function trimfield(str) {
    return str.replace(/^\s+|\s+$/g, '');
}