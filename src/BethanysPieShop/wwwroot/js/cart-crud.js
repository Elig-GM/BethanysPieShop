const ROOT_API_CART = "/api/Cart/";
var respons = "";

function getShoppingCart() {
    $.ajax({
        type: 'GET',
        url: ROOT_API_CART,
        dataType: 'json',
        success: function (cartItems) {
            var li_NavCart = null;
            // var siz = $(document).innerWidth();
            if (cartItems.pies.length == 0) {
                $("#li-NavCart").empty();
                $("#li-NavCart2").empty();
            } else {
                if ($(document).innerWidth() <= 974 && $(document).innerWidth() > 327) {
                    li_NavCart = "#li-NavCart";
                } else {
                    li_NavCart = "#li-NavCart2";
                }
                $("#li-NavCart").empty();
                $("#li-NavCart2").empty();
                $(  '<a class="nav-link dropdown-toggle" style="cursor : pointer;" data-toggle="dropdown" id="navCart">' +
                    '   <i class="fa fa-shopping-cart"></i>' +
                    '   <span class="badge">' + cartItems.count + '</span>' +
                    '</a>' +
                    '<div class="dropdown-menu dropdown-menu-right box-shadow py-0" id="dropdownM_NavCart" aria-labelledby="navbarDropdown">' +
                    '   <div class="col-md-12 px-1">' +
                    '       <a href="" style="color: #777777; text-decoration: none; vertical-align: middle;" onclick="redirect()">' +
                    '           <h5 class="d-flex justify-content-between align-items-center my-3">' +
                    '               <span class="text-muted px-4">Your cart</span>' +
                    '               <a class="mr-4 cart" href="" onclick="redirect()" style="color: #6c757d; text-decoration: none; vertical-align: middle;">' +
                    '                   <i class="fa fa-shopping-cart"></i>' +
                    '                   <span class="badge" id="itemsCount">' + cartItems.countT + '</span>' +
                    '               </a>' +
                    '           </h5>' +
                    '       </a>' +
                    '       <div class="dropdown-divider mb-0 mx-3"></div>' +
                    '       <ul class="list-group mb-1">' +
                    '           <div id="itemsCart"></div>' +
                    '           <div class="dropdown-divider mb-0 mx-3"></div>' +
                    '           <li class="list-group-item d-flex justify-content-between py-2" style=" border: 0px;">' +
                    '               <span>Total (USD)</span>' +
                    '               <strong>$' + cartItems.total + '</strong>' +
                    '           </li>' +
                    '       </ul>' +
                    '   </div>' +
                    '</div>').appendTo($(li_NavCart));

                // document.getElementById(li_NavCart).innerHTML = 


                $("#itemsCart").empty();
                $.each(cartItems.pies, function (index, cart) {
                    // cart.pie.shortDescription
                    $(  '<li class="list-group-item d-flex justify-content-between lh-condensed  pb-3 px-3" style=" border: 0px;">' +
                        '   <div class="row">' +
                        '       <div class="col-1 justify-content-center align-self-center">' +
                        '           <h6 class="my-0">' +
                        '               <span style="background: #3bb18f;" class="badge badge-secondary badge-pill">' + cart.amount + '</span>' +
                        '           </h6>' +
                        '       </div>' +
                        '       <div class="col">' +
                        '           <h6 class="my-0" href="" style="text-decoration: none; vertical-align: middle; cursor: pointer;"  onclick="redirect(' + cart.pie.pieId + ')" >' +
                                        cart.pie.name +
                        '           </h6>' +
                        // '           <div class="d-flex justify-content-between lh-condensed">' +
                        // '           <small class="text-muted">$' + cart.pie.price + '</small>' +
                        '           <small class="text-muted">$' + parseFloat(Math.round((cart.pie.price * cart.amount) * 100) / 100).toFixed(2).toString().replace(/[.]/, ",") + ' ($' + cart.pie.price.toLocaleString() + ')</small>' +
                        // '               <span class="text-muted ">$' + cart.pie.price * cart.amount + '</span>' +
                        // '           </div>' +
                        '       </div>' +
                        '   </div>' +
                        '   <div class="d-flex justify-content-between lh-condensed">' +
                        '       <a class="justify-content-center align-self-center" onclick="removeFromShoppingCart(' + cart.pie.pieId + ', 1)"' +
                        '           style="color: #545b62;">' +
                        '           <i class="far fa-trash-alt fa-xs ml-2 mr-1 align-middle my-auto" style="cursor : pointer;"></i>' +
                        '       </a>' +
                        '   </div>' +
                        '</li>').appendTo($("#itemsCart"));
                });
            }
        }
    });
}

function getShopping_Cart() {
    $.ajax({
        type: 'GET',
        url: ROOT_API_CART,
        dataType: 'json',
        success: function (cartItems) {
            document.getElementById('cartTotal').innerHTML = "$" + cartItems.total;
            document.getElementById('cartTotal2').innerHTML = "$" + cartItems.total;
            $("#tableCart").empty();
            $.each(cartItems.pies, function (index, cart) {
                $(  '<tr class="g-brd-bottom g-brd-gray-light-v3">' +
                    '   <td class="text-left g-py-25">' +
                    '       <img class="d-inline-block g-width-100 mr-4" src="' + cart.pie.imageThumbnailUrl + '" alt="Image Description">' +
                    '       <div class="d-inline-block align-middle">' +
                    '           <h4 class="h6 g-color-black">' + cart.pie.name + '</h4>'+ 
                    '           <ul class="list-unstyled g-color-gray-dark-v4 g-font-size-12 g-line-height-1_6 mb-0">'+ 
                    '               <li>Color: Black</li>'+ 
                    '               <li>Size: MD</li>'+ 
                    '           </ul>'+ 
                    '       </div>'+ 
                    '   </td>' + 
                    '   <td class="g-color-gray-dark-v2 g-font-size-13">$' + cart.pie.price.toLocaleString() + '</td>' + 
                    '   <td class="text-center">' + 
                    '       <div class="js-quantity input-group u-quantity-v1 g-width-80 g-brd-primary--focus mx-0">' + 
                    '           <div class="input-group-addon d-flex align-items-center g-width-80 g-brd-gray-light-v2 g-bg-white g-font-size-12 rounded-0 g-px-5 g-py-6">' + 
                    '               <i class="js-plus g-color-gray g-color-primary--hover mr-2 fas fa-plus" onclick="addToShopping_Cart('+ cart.pie.pieId +')"></i>' + 
                    '           <input class="js-result form-control text-center g-font-size-13 rounded-0 g-pa-0" id="amou" data-id="'+ cart.pie.pieId + '" type="text" value="' + cart.amount + '" readonly>' + 
                    '               <i class="js-minus g-color-gray g-color-primary--hover ml-2 fas fa-minus" onclick="removeFromShopping_Cart('+ cart.pie.pieId +')"></i>' + 
                    '           </div>' + 
                    '       </div>' + 
                    '   </td>' + 
                    '   <td class="text-right g-color-black">' + 
                    '       <span class="g-color-gray-dark-v2 g-font-size-13 mr-4">$' + parseFloat(Math.round((cart.pie.price * cart.amount) * 100) / 100).toFixed(2).toString().replace(/[.]/, ",") + '</span>' + 
                    '       <a onclick="removeFromShopping_Cart(' + cart.pie.pieId + ', 1)">' + 
                    '           <span class="g-color-gray-dark-v4 g-color-black--hover g-cursor-pointer">' + 
                    '               <i class="mt-auto fa fa-trash" id="js-del"></i>' + 
                    '           </span>' + 
                    '       </a>' + 
                    '   </td>' + 
                    '</tr>' ).appendTo($("#tableCart"));
            });
        },
        error(){

        }
    });
}
function removeFromShopping_Cart(pieID, amount) {
    $.ajax({
        method: (amount == 1) ? "DELETE" : "PUT",
        url: ROOT_API_CART + (pieID || ""),
        dataType: 'json',
        success: function () {
            getShoppingCart();
            getShopping_Cart();
        },
        error: function () {}
    });
}

// function getShoppingCart() {
//     $.ajax({
//         type: 'GET',
//         url: ROOT_API_CART,
//         dataType: 'json',
//         success: function (cartItems) {

//             // if(cartItems){
//             if (cartItems.pies.length == 0) {
//                 // $("#li-NavCart").empty();
//                 // $("#li-NavCart2").empty();
//                 $('#li_NavCartR').css({ 'display': 'none' });
//                 $('#li_NavCart').css({ 'display': 'none' });
//             } else {
//                 if ($(document).width() < 990 && $(document).width() > 327) {
//                     $('#li_NavCart').css({ 'display': 'none' });
//                     $('#li_NavCartR').css({ 'display': 'block' });
//                 } else {
//                     $('#li_NavCartR').css({ 'display': 'none' });
//                     $('#li_NavCart').css({ 'display': 'block' });
//                 }
//                 // document.getElementById("itemsCount_NavCart").innerHTML = cartItems.count;
//                 // document.getElementById("totalP_NavCart").innerHTML = "$" + cartItems.total;
//                 // $("#items_NavCart").empty();
//                 $.each(cartItems.pies, function (index, cart) {

//                     $(  '<li class="list-group-item d-flex justify-content-between lh-condensed  py-2 px-3">' +
//                         '   <div>' +
//                         '       <h6 class="my-0">' +
//                         '           <span style="background: #3bb18f;" class="badge badge-secondary badge-pill mr-1">' + cart.amount + '</span>' +
//                                     cart.pie.name +
//                         '       </h6>' +
//                         '       <small class="text-muted">' + cart.pie.shortDescription + '</small>' +
//                         '   </div>' +
//                         '   <div class="d-flex justify-content-between lh-condensed">' +
//                         '       <span class="text-muted ">$' + cart.pie.price + '</span>' +
//                         '       <a class="justify-content-center align-self-center" onclick="removeFromShoppingCart(' + cart.pie.pieId + ')"' +
//                         '           style="color: #545b62;">' +
//                         '           <i class="far fa-trash-alt ml-2 align-middle my-auto"></i>' +
//                         '       </a>' +
//                         '   </div>' +
//                         '</li>').appendTo($("#itemsNavCart2"));
//                         alert("hola");
//                 });
//             }
//         }
//         // }
//     });
// }

function addToShopping_Cart(pieID, amount) {
    if (amount == null)
        amount = 1;
    else
        document.getElementById('amount').value = 1;

    $.ajax({
        method: "POST",
        url: ROOT_API_CART + (pieID || ""),
        contentType: "application/json",
        dataType: 'json',
        data: JSON.stringify(amount),
        success: function () {
            getShopping_Cart();
            toastr.options = {
                "positionClass": "toast-bottom-right"
            };
            toastr.success("Pie added to the shopping cart!");
        },
        error: function (msg) {
            // toastr.error('An error occured while trying to save the user.');
        }
    });
}

function addToShoppingCart(pieID, amount) {
    if (amount == null)
        amount = 1;
    else
        document.getElementById('amount').value = 1;

    $.ajax({
        method: "POST",
        url: ROOT_API_CART + (pieID || ""),
        contentType: "application/json",
        dataType: 'json',
        data: JSON.stringify(amount),
        success: function () {
            getShoppingCart();
            toastr.options = {
                "positionClass": "toast-bottom-right"
            };
            toastr.success("Pie added to the shopping cart!");
        },
        error: function (msg) {
            // toastr.error('An error occured while trying to save the user.');
        }
    });
}

function removeFromShoppingCart(pieID, amount) {
    $.ajax({
        method: (amount == 1) ? "DELETE" : "PUT",
        url: ROOT_API_CART + (pieID || ""),
        dataType: 'json',
        success: function () {
            getShoppingCart();
        },
        error: function () {}
    });
}

function redirect(id) {
    if (id == null) {
        $.ajax({
            method: "GET",
            url: ROOT_API_CART + "5",
            dataType: 'json',
            success: function (result) {
                window.location = result.url;
            },
            error: function (msg) {
                // toastr.error('An error occured while trying to save the user.');
            }
        });
    } else {
        window.location = "/Pie/Details/" + id;
    }
}

$(document).ready(function () {
    if ($(document).innerWidth() <= 974 && $(document).innerWidth() > 327) {
        respons = "mobile"
    } else {
        respons = "pc";
    }
    getShoppingCart();
});

$(window).resize(function () {
    // alert("resize");
    if ($(document).innerWidth() <= 974 && $(document).innerWidth() > 327) {
        var mobile = "mobile";
        if (respons != mobile) {
            getShoppingCart();
            respons = mobile;
        }
    } else {
        var pc = "pc";
        if (respons != pc) {
            getShoppingCart();
            respons = pc;
        }
    }
});


