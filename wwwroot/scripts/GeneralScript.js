

// Change background LETUSKNOW on aboutPage Below

const currentUrl = window.location.href;

if (currentUrl.slice(-8) == "contact/"){
    
    const LetUsKnowAboutPage = document.querySelector(".let-us-know-container");

    LetUsKnowAboutPage.style.backgroundColor = "white";
    
}





// DropDownMenu Below!!

const dropDownButton = document.querySelector(".fa-bars");

const dropDownMenu = document.querySelector(".containerDropDown");

dropDownButton.addEventListener("mouseover", (e) => {

        dropDownMenu.style.display = "flex";

        setTimeout(() => {

            dropDownMenu.style.opacity = "1";
            e.target.style.fontSize = "2.5rem";

        }, 100);

       
        

});

dropDownButton.addEventListener("mouseout", (e) => {

       
        dropDownMenu.style.display = "none";

        setTimeout(() => {

            dropDownMenu.style.opacity = "0";
            e.target.style.fontSize = "2rem";

        }, 100);

       

});

dropDownMenu.addEventListener("mouseover", (e) => {

        
        dropDownMenu.style.display = "flex";

        setTimeout(() => {

            dropDownMenu.style.opacity = "1";

        }, 100);

    
});

dropDownMenu.addEventListener("mouseout", (e) => {

        
        dropDownMenu.style.display = "none";

        setTimeout(() => {

            dropDownMenu.style.opacity = "0";

        }, 100);


});




// Below Scroll contactFORM into view!



function ScrollContactIntoView(){

    let contactElement = document.querySelectorAll(".contact-page-form-container");

    contactElement.scrollIntoView({ behavior: "smooth" });

}



