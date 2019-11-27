﻿
    

  


var app = angular.module('RegApp', []);
app.controller('RegistrationController', function ($scope, $http, $window) {
  
    $scope.Ptemail = "([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z]+\.)+([a-zA-Z]{2,3})";

    $window.onload = function () {
        $scope.Regg = {};
        $http.get("/Home/GetGruopName/").then(function (response) {
            $scope.Regg = response.data;
          //  alert(response.data);

            
        });
    }

    $scope.Home = function () {
        alert()
    }
 

    $scope.newValue = function (v) {
        $scope.ms = {};
        if ($scope.Registration.rtype == "NewGroup") {
                   
            $scope.ms.NewG = true;
            $scope.ms.Mem = false;
            $scope.ms.Comp = false;
            $scope.ms.ProFa = false;
            $scope.Registration.gname = "";
          
        } else if ($scope.Registration.rtype == "Member") {
            $scope.ms.NewG = false;
            $scope.ms.Comp = false;
            $scope.ms.ProFa = false;
            $scope.ms.Mem = true;

        } else// if ($scope.Registration.rtype == "ProgressiveFarmer") {
        //    $scope.ms.NewG = false;
        //    $scope.ms.Mem = false;
        //    $scope.ms.Comp = false;
        //    $scope.ms.ProFa = true;
        //    $scope.Registration.gname = "";
            //} else
            {
            $scope.ms.NewG = false;
            $scope.ms.Mem = false;
            $scope.ms.ProFa = false;
            $scope.ms.Comp = true;
            $scope.Registration.gname = "";
        }
    }

    $scope.gpname = function () {
      
        $scope.Registration.gname = $scope.Registration.gname;
        alert($scope.Registration.gname);
    }
 

    

    $scope.savedata = function () {
               
        $http({
            method: 'POST',

            url: '/Home/Register/',

            data: $scope.Registration

        }).then(function (response) {
            $scope.msk = {};
            $scope.btntext = "Save";

            $scope.err = response.data;

            if ($scope.err == "Success") {
               
                //$scope.Registration.name=null;
                //$scope.Registration.gname=null;
                //$scope.Registration.addr = null;
                //$scope.Registration.email = null;
                //$scope.Registration.monum = null;
                //$scope.Registration.pass = null;
                //$scope.pss = null;
               // $scope.Registration = null;
                alert("Your Registration Successfully ... !");
                $window.location.href = '/Home/Registraionall/';
            }
            else {
               // $scope.msk.fail = true;
                alert("Your Registration Not Successfully ! Plase Trial Agin...");
            }
        });
                      
    }
//Grop name exit or not
    $scope.GroupNameExit = function () {
        $http({
            url: "/Home/GroupNameExit/",
            method: "POST",
            params: { gnm: $scope.Registration.gname }
        }).then(function (response) {
            $scope.Groupnm = response.data;
        });

    }
    //Email Id exit or not
    $scope.EmailExit = function () {
        $http({
            url: "/Home/EmailExit/",
            method: "POST",
            params: { em: $scope.Registration.email }
        }).then(function (response) {
            $scope.Exemail = response.data;
        });

    }

    //Mobile No exit or not
    $scope.MonoExit = function () {
        $http({
            url: "/Home/MonoExit/",
            method: "POST",
            params: { mo: $scope.Registration.monum }
        }).then(function (response) {
            $scope.Exmonum = response.data;
        });

    }




});


//display for Requset of New Soicety and Compny
var ap = angular.module('RegAdminReq', []);
ap.controller('ReqAdminController', function ($scope, $http, $window) {
    $scope.btn = "Save";
    GetNewGroup();
    GetCompany();
    ItemTypeGet();
    //get for database to display accept or reject New group allow in system
    function GetNewGroup() {

        $http.get("/Home/GetNewGP/").then(function (response) {
            $scope.cnt = 1;
            $scope.Reg = response.data;
       
        }, function () {
            alert('Data not found');
        });

    }

    //get for database to display accept or reject company allow in system
    function GetCompany() {

        $http.get("/Home/GetNewCP/").then(function (response) {

            $scope.Regn = response.data;

        }, function () {
            alert('Data not found');
        });

    }
    //accept requset for new group and company
    $scope.AppNewGroupComp = function (Re) {

        $scope.spin = {};
        //$scope.btntext = "PalseWait";
        $scope.spin.kk = true;
   
        if ($window.confirm("Do you want to continue?")) {
            $scope.roundd = true;
             $http({
            url: "/Home/AcceptNewGroup/",
            method: "POST",
            params: { rid: Re.rid }
        }).then(function (response) {
            alert(response.data);
            $scope.spin.kk = false;
            $scope.roundd = false;
            //$scope.btntext = "Accept";
            GetNewGroup();
            GetCompany();
        });

        }      
    }

     //Reject requset for new group and company
    $scope.RejectCompanyNewGroup = function (Re) {
       
       
        if ($window.confirm("Do you want to continue?")) {
            $scope.roundd = true;
            $http({
                url: "/Home/RejectNewCmp/",
                method: "POST",
                params: { rid: Re.rid }
            }).then(function (response) {
                alert(response.data);   
                $scope.roundd = false;
                GetNewGroup();
                GetCompany();
            });

        }
    }

    $scope.AddType = function () {
        var getAction = $scope.Action;
        if (getAction != "Update") {

            $scope.btn = "Plese wait";
            $scope.spin = {};
            $scope.spin.kk = true;


            $http({
                method: 'POST',

                url: '/Home/AddItemType/',

                data: $scope.ItemType

            }).then(function (response) {


                $scope.msk = {};
                $scope.btn = "Save";
                $scope.spin.kk = false;

                $scope.err = response.data;

                if ($scope.err == "success") {
                    $scope.ItemType = null;
                    $scope.msk.add = true;
                    ItemTypeGet();
                } else {
                    $scope.msk.fail = true;
                }

            });
        } else {
            //alert("updater");
            $scope.spin = {};
            $scope.spin.kk = true;

            $scope.btn = "Please wait...!";
            //update code
            $http({
                method: "post",
                url: "/Home/EditeIype/",
                data: JSON.stringify($scope.ItemType),
                dataType: "json"
            }).then(function (respons) {
                alert(respons.data);
                $scope.ItemType =null;
                $scope.spin.kk = false;
                $scope.btn = "Save";
                $scope.Action = null;
               
                ItemTypeGet();

            });
        }
    }
    //get Itemtype from itemtypetable
    function ItemTypeGet() {

        $http.get("/Home/GetItemType/").then(function (response) {
                     $scope.Itype = response.data;
        }, function () {
            alert('Data not found');
        });

    }

    //get from value in textbox for edit Itemtype
    $scope.getType = function (Re) {

       
        //  $scope.Reg.name = re.id;
        $scope.ItemType = {};
        $scope.ItemType.ItypeId = Re.ItypeId;
        $scope.ItemType.type = Re.type,
          

        $scope.Action = "Update";
        $scope.btn = "Update";

    }

    //delete code but stsus is 1 or 0 set
    $scope.deleteItemType = function (ab) {
    
        if ($window.confirm("Do you want to Delete Record?")) {
            $http({
                url: "/Home/DeleteType/",
                method: "POST",
                params: { id: ab.ItypeId }
            }).then(function (response) {
                alert(response.data);

                ItemTypeGet();
            });

        }
    }

    ///code of allredy exit or not item type of admin
    $scope.ItemCheck = function () {
       // alert($scope.ItemType.type)

        $http({
            url: "/Home/ItemTypExit/",
            method: "GEt",
            params: { nm: $scope.ItemType.type }
        }).then(function (response) {
            $scope.typeIm = response.data;

           
        });

    }


});


//Login of each user


var LOGIN = angular.module('UserLogin', ['base64']);
LOGIN.controller('UserLOG', function ($scope, $http, $window, $base64) {
    $scope.ms = {};
    $scope.DaImg = null;
    $scope.ShowLoader = false;
    GetCp();


   function GetCp(){
        var nn = "1234567890";
        var x = nn[Math.floor(Math.random() * nn.length)];

        for (var i = 1; i< 4; i++) {
            x = x + nn[Math.floor(Math.random() * nn.length)];
        }
       $scope.DaImg = x;
            
    }
    $scope.cpclick = function () {
        GetCp();
    }
   

 

    $scope.Loginuser = function () {

       
        $scope.roundd = true;
        $http({
            url: "/Home/ValidLogin/",
            method: "POST",
            data: $scope.Registration
        }).then(function (response) {
           
            $scope.field = response.data;
            if ($scope.field == "worng") {
                $scope.ms.fiel = true;
                $scope.roundd = false;
                //alert($scope.ms.fiel);
                $scope.mss = "Invaid credentials try again ..!";
               
            } else {
                //setTimeout(function () { $scope.ShowLoader = true; }, 10000);
                $scope.roundd = false;
                if ($scope.field.rtype == "NewGroup") {
                   
                    $window.location.href = '/Home/Index/';

                } else if ($scope.field.rtype == "Company") {
                    $window.location.href = '/Home/Index/';
                } else if ($scope.field.rtype == "Member") {
                    $window.location.href = '/Home/Index/';
                } else {
                    $window.location.href = '/Home/Index/';
                }
            }

        });
    }
});



//each user can add Item like seed,fertilizer
var Item = angular.module('AddItem', ['lr.upload', 'ui.directives', 'ui.filters']);
Item.controller('AddItemUSerController', ['$scope', 'upload', '$http', function ($scope, upload, $http, $window) {
    // $scope.validext = "([^\\s]+(\\.(?i)(jpg|png|gif|bmp))$)";
    $scope.cluNameId = '';
    $scope.btnn=' Save'
    CropName();
    CropNameClu();
    Cropvarityget();
    CropNameGroup();
    $scope.toclu = false;
    $scope.init = function () {
        $http.get("/Home/GetItemTypeReg/").then(function (response) {
            $scope.Itype = response.data;

        });
    }
    //remove cookie
    $scope.Cook = function () {
        $http.get("/Home/RemoveCok/");
    }

    //crop name get from database
   function CropName() {
        $scope.cpn = {};
        $http.get("/Home/GetCropName/").then(function (response) {
            $scope.san = response.data;
            //  alert(response.data);


        });
    }

    function CropNameGroup() {
        $scope.cpn = {};
        $http.get("/Home/GetCropNameGr/").then(function (response) {
            $scope.sann = response.data;
            //  alert(response.data);


        });
    }
    //crop name get from database CALUTION
    function CropNameClu() {
        $scope.cpnn = {};
        $http.get("/Home/GetCropNameClu/").then(function (response) {
            $scope.cpnn = response.data;
            //  alert(response.data);


        });
    }

    //crop varity add by admin
    $scope.AddVerity = function (Varity) {
        var getAc = $scope.Action;
        if (getAc != "Update") {

            $http({
                url: "/Company/AddCropVerity/",
                method: "POST",
                data: Varity,
                dataType: "json",
            }).then(function (response) {
                alert(response.data);
                $scope.Varity.cpid = null;
                $scope.Varity.verity = null;
                Cropvarityget();
            });
        } else {
            //$scope.spin = {};
            //$scope.spin.kk = true;
            //$scope.AddCrop.ItypeId = $scope.ItemMasert.ItypeId;
            $scope.btnn = "Please wait...!";
            //update code
            $http({
                method: "post",
                url: "/Company/VarityEditePost/",
                data: JSON.stringify($scope.Varity),
                dataType: "json"
            }).then(function (respons) {
                alert(respons.data);
                //$scope.ItemType = null;
                //$scope.spin.kk = false;
                $scope.btnn = "Save";
                $scope.Varity.cpid = null;
                $scope.Varity.verity = null;
                //$scope.AddCrop = null;
                Cropvarityget();

            });

        }

    }
    //get crop varity 
    function Cropvarityget() {

        $http.get("/Company/GetVerity/").then(function (response) {
            $scope.GetVar = response.data;
        }, function () {
            alert('Data not found');
        });

    }
    //edite varity
    $scope.getCropvarityEdit = function (R) {
        $scope.Varity = {};
        //  alert(Re.cpname)
        $scope.Varity.ItypeId = R.ItypeId;
        $scope.Varity.cpid = R.cpid;
        $scope.cpnm = R.cpname;
        $scope.Varity.verity = R.verity;
        $scope.Varity.varid = R.varid;

      //  alert(R.cpid)
            $scope.Action = "Update";
        $scope.btnn = "Update";
        
    }
    //delete varity
    $scope.deletteCropVarity = function (ab) {

        if (confirm("Do you want to Delete Record?")) {
            $http({
                url: "/Company/DeleteCropVar/",
                method: "POST",
                params: { id: ab.varid }
            }).then(function (response) {
                alert(response.data);

                Cropvarityget();
            });

        }
    }
    //chek varity exit or not
    $scope.CropVaritExit = function () {
        $http({
            url: "/Company/CropVarityExit/",
            method: "POST",
            params: { id: $scope.Varity.cpid, nam: $scope.Varity.verity }
        }).then(function (response) {
            if (response.data == "exit") {
                alert("Varity Name is already use try other name .!");
                $scope.Varity.verity = null;
            }
           
        });

    
    }

    //crop varity get ajax in addregitm

    $scope.SelectCrop = function () {
     //   alert($scope.ItemMasert.cpid)
        $http({
            url: "/Company/CropVarGet/",
            method: "POST",
            params: { id: $scope.ItemMasert.cpid,}
        }).then(function (response) {
            $scope.varname = response.data;
           
        });


    }

    $scope.SelectCropNew = function () {
        //   alert($scope.ItemMasert.cpid)
        $http({
            url: "/Company/CropVarGetNew/",
            method: "POST",
            params: { id: $scope.ItemMasert.cpid, }
        }).then(function (response) {
            $scope.varname = response.data;

        });


    }


    $scope.cal = function () {
        $scope.mytt = {};
        $scope.sa = '';
        $http({
            url: "/Home/SubCropCluId/",
            method: "POST",
            params: { id: $scope.ItemMasert.varkeyItem }
        }).then(function (response) {
            $scope.mytt = response.data;
           // console.log($scope.mytt.Imid);
           // alert('len '+response.data.length);
            for (i = 0; i < $scope.mytt.length; i++) {
                $scope.sa += $scope.mytt[i].Imid + ','
            }

            $scope.cluNameId = $scope.sa;

        });
       
    }
    //claculation for submited crop
    $scope.CalculateCrop = function () {
       // alert($scope.ItemMasert.cpid)
        $http({
            url: "/Home/SubCropClu/",
            method: "POST",
            params: { id: $scope.ItemMasert.varkeyItem}
        }).then(function (response) {
            if (response.data == '0') {
              //  alert()
            } else {
                $scope.toclu = true;
                $scope.ItemMasert.qty = response.data;
            }


        });
    }

    //membar perpos
    $scope.AddItemUser = function (ItemMasert) {
        $scope.ItemMasert.img = $scope.currentFile;
      
      //  if ($scope.currentFile.na)
      //  alert($scope.ItemMasert.img);
        upload({
            url: "/Home/AddItem/",
            method: "POST",
            data: ItemMasert,
            dataType: "json",
        }).then(function (response) {
            
            $scope.ItemMasert = null;
            if (response.data == "success") {
                alert('Your Item Added successfully')
          
                window.location = "/Secretary/ShowMemabersp";
                
            } else {
                alert(response.data)
            }
            
        });
    }
    //sell or other
    $scope.AddUserItem = function (ItemMasert) {
        $scope.ItemMasert.img = $scope.currentFile;
      //  alert($scope.sa)
       
      //  alert($scope.ItemMasert.img);
        upload({
            url: "/Company/AdddItem/",
            method: "POST",
            data: ItemMasert,
            dataType: "json",
        }).then(function (response) {
            $scope.ItemMasert = null;
          
                alert(response.data)
            

        });
    }


    $scope.uploadedFile = function (element) {
        $scope.currentFile = element.files[0];
      //  $scope.ItemMasert.img = $scope.currentFile.name;
        if (element.files[0].type == 'image/jpeg' || element.files[0].type == 'image/png') {
            $scope.ItemMasert.img = $scope.currentFile;
        } else {
          //  element.files[0] = null;
            $scope.ItemMasert.img = null;
        }
      //  alert(element.files[0].type)
        //alert($scope.currentFile);
    }

    //id thru fetch name of membar and group
    $scope.getdetails = function () {
        $http({
            url: "/Secretary/GetMemabrDetails/",
            method: "POST",
            params: { id: $scope.Memid }
        }).then(function (response) {
           // alert();
            $scope.Reg = response.data;
           
           
        });
    }
}]);



var Member = angular.module('RegScerpatryReq', []);
Member.controller('ReqScerpatryController', function ($scope, $http, $window, $location) {
    GetMemaber();
    GetMemaberGroup();
    function GetMemaber() {

        $http.get("/Secretary/GetMemaberSp/").then(function (response) {
            
            $scope.Mem = response.data;

        }, function () {
            alert('Data not found');
        });

    }
    function GetMemaberGroup() {

        $http.get("/Secretary/GetMemaberGroup/").then(function (response) {

            $scope.Memm = response.data;

        }, function () {
            alert('Data not found');
        });

    }

   


    $scope.AppMembar = function (Re) {
      //  alert(Re.rid)

        if ($window.confirm("Are you sure?")) {
            $scope.roundd = true;
            $http({
                url: "/Secretary/AcceptMembar/",
                method: "POST",
                params: { id: Re.rid }
            }).then(function (response) {
                alert(response.data);
                $scope.roundd = false;
                GetMemaber();
            });

        }
    }

    $scope.ItemAdd = function (r) {
         $window.location.href = ("/Company/RegItem/?id="+r.rid+"&&nm="+r.name);

    }

    $scope.DeleteMem = function (a) {
        if (window.confirm("Are you sure?")) {

            $http({
                url: "/Secretary/DeletMembar/",
                method: "POST",
                params: { id: a.rid }
            }).then(function (response) {
                alert(response.data);
                GetMemaberGroup();
            });

        }

    }

    $scope.RejectMembar = function (a) {
        if (window.confirm("Are you sure?")) {

            $http({
                url: "/Secretary/RejectMembar/",
                method: "POST",
                params: { id: a.rid }
            }).then(function (response) {
                alert(response.data);
                GetMemaber();
            });

        }
    }

});



//show item for secretry selling item
var show = angular.module('ShowItemSecertry', []);
show.controller('ShowItemSecertryController', function ($scope, $http, $window) {
    GetShowItem();

    function GetShowItem() {
        $http.get("/Secretary/GetShowItem/").then(function (response) {

            $scope.Item = response.data;

        }, function () {
            alert('Data not found');
        });
    }
    $scope.getImg = function () {

        $scope.show = {};
        if ($scope.aa == "OurItem") {
            GetShowItem();
        } else {
           
            $http.get("/Secretary/GetShowItemBuy/").then(function (response) {

                $scope.Item = response.data;
                $scope.show.buy = true;
            });
        }
       
    }

    $scope.AddCartItem = function (Cr) {
        if ($window.confirm("Are you sure?")) {
            $http({
                url: "/Company/AddCart/",
                method: "POST",
                params: { id: Cr.Imid, ab: Cr.varid }
            }).then(function (response) {
            
                window.location = "/Secretary/ShowSellingItem/"; 
            });

        }
    }

   
});


//Add Item Added show
var showItem = angular.module('ShowItem', ['lr.upload']);
showItem.controller('ShowItemController', ['$scope', 'upload', '$http', function ($scope, upload, $http, $window)  {
    GetData();
    ItemTypeGettt();
    function GetData() {
        $http.get("/Secretary/GetItemData/").then(function (response) {

            $scope.ItemData = response.data;

        }, function () {
            alert('Data not found');
        });
    }
    $scope.ShowDeta = function () {
        $http.get("/Secretary/GetItemDataCm/").then(function (response) {

            $scope.ItemData = response.data;

        }, function () {
            alert('Data not found');
        });
    }

    function ItemTypeGettt() {

        $http.get("/Home/GetItemType/").then(function (response) {
            $scope.Itypee = response.data;
        }, function () {
            alert('Data not found');
        });

    }

    $scope.ShowDetails = function () {
        if ($scope.aa == "1") {
            GetData();
        } else if ($scope.aa == "Requirement") {

            $http.get("/Secretary/GetShowRequirement/").then(function (response) {

                $scope.ItemData = response.data;
                $scope.show.buy = true;
            });
        } else {
            $http.get("/Secretary/GetShowSubmitCrop/").then(function (response) {

                $scope.ItemData = response.data;
                $scope.show.buy = true;
            });
        }
    }

    //edite item display
    $scope.Item= function () {
        $http.get("/Company/SellItemEditeUser/").then(function (response) {
             
            $scope.ItemMasert = response.data;
            $scope.AddCrop = response.data;
            //alert(response.data.verity);
        }, function () {
            alert('Data not found');
        });

        $scope.ItemEdite = function (ItemMasert) {
            if ($scope.currentFile != null) {
                $scope.ItemMasert.img = $scope.currentFile;
            }
          //  alert()
            upload({
                url: "/Company/EditeSellItem/",
                method: "POST",
                data: ItemMasert,
                dataType: "json",
            }).then(function (response) {

                alert(response.data)
                window.location = "/Secretary/Show";

            });
        }
      
        
    }

    //delete item
    $scope.DeletItem = function (a) {
       // alert(a.Imid)
     
       if(window.confirm("Are you sure?")) {

        $http({
            url: "/Company/DeletItem/",
            method: "POST",
            params: { id: a.Imid }
        }).then(function (response) {
                alert(response.data);
                GetData();
            });

        }

    }


    $scope.uploadedFile = function (element) {
        $scope.currentFile = element.files[0];
       // alert($scope.currentFile);
    }

    //payment details of submittec crop and requrment crop
    $scope.Payment = function (m) {
        if ($scope.aa == "Requirement") {
            alert('Payment Accept :-' + m.price * m.qty + ' Rs')
            if (window.confirm("Are you sure Payment?")) {
                $http({
                    url: "/Secretary/PaymentMembar/",
                    method: "POST",
                    data: m,
                    dataType: "json",
                }).then(function (response) {
                    alert(response.data);
                    GetData();
                });
            }

        } else {
            alert('Give Payment to Membar  :-' + m.price * m.qty + ' Rs')
            if (window.confirm("Are you sure Payment?")) {
                $http({
                    url: "/Secretary/PaymentMembar/",
                    method: "POST",
                    data: m,
                    dataType: "json",
                }).then(function (response) {
                    alert(response.data);
                    GetData();
                });
            }
        }
      
    }

}]);



//show cart details
var showItem = angular.module('ShowCart', []);
showItem.controller('ShowCartController', function ($scope, $http, $window) {
    GetCartDetails();

    

    $scope.count = 0;
    var k = 0;
    var ab = 0;
    var y = 0;
    $scope.isDisabled = true;
    $scope.Disabled = false;
    $scope.crtid = "";
    $scope.Qunty = "";
    $scope.list = [];
    $scope.cartid = [];
    function GetCartDetails() {
        $http.get("/Company/GetCartDetails/").then(function (response) {


            $scope.Car = response.data;
            // alert($scope.Car);

        }, function () {
            alert('Data not found');
        });

    }

    //code for order button 
    $scope.ConDt = function () {
        $http.get("/Company/GetCartButton/").then(function (response) {


            if (response.data == 0) {
                $scope.btsu = false;
            } else {
                $scope.btsu = true;
               // alert(response.data)
            }
            

        }, function () {
            alert('Data not found');
        });
    }

    //code of add cart and calculation 

    $scope.AddItemCart = function (a, count) {
        //k += a.price;
        //$scope.total = k;

        //$scope.crtid += a.cid + ",";
        //$scope.Qunty +=  count + ",";     
      //  alert(a.cid);
        $http({
            url: "/Company/CartItemIncre/",
            method: "POST",
            params: { id: a.cid }
        }).then(function (response) {
            GetCartDetails();
        });
        
    
}

    //drecrement code for total amount
    $scope.AddDecrementCart = function (a) {

        //alert(k)
        //k -= a.price;
        //$scope.total = k;
        //$scope.crtid += a.cid + ",";
       
        //$scope.Qunty +=  count + ",";     
      //  alert(a.cid);
        $http({
            url: "/Company/CartItemDecre/",
            method: "POST",
            params: { id: a.cid }
        }).then(function (response) {
            GetCartDetails();
        });

             
    }
    $scope.CartRemove = function (a,tot) {
      //  alert(tot)
       
        if ($window.confirm("Are you sure?")) {
           
            $http({
                url: "/Company/CartRemove/",
                method: "POST",
                params: { id: a.cid }
            }).then(function (response) {
                alert(response.data);
             
                k = 0;
                $scope.total = k;
                GetCartDetails();
            });

        }

    }
    //disp totl
    $scope.getTotal = function () {
        var total = 0;
        for (var i = 0; i < $scope.Car.length; i++) {
            var product = $scope.Car[i];
            total += (product.price * product.qt);
        }
        return total;
    }


    //confrim order
    $scope.ConfrimOredr = function () {
       
       
        if ($window.confirm("Are you sure?")) {
            $http({
                url: "/Company/CorfrimOrder/",
                method: "POST",
                params: { id: $scope.crtid, qt: $scope.Qunty }
            }).then(function (response) {
                $scope.Disabled = true;
                $scope.isDisabled = false;
                $scope.btsu = false;
                alert(response.data);
                GetCartDetails();
              
            });

        }
        
    }

    //$scope.Billprint = function () {
    //    $http({
    //        url: "/Company/PrintBill/",
    //        method: "POST",
           
    //    }).then(function (response) {
    //        alert(response.data);
    //        //GetCartDetails();
    //    });
    //}

    $scope.OredrCancel = function () {

        if ($window.confirm("Are you sure?")) {
            $http({
                url: "/Company/OredrCencel/",
                method: "POST",
            }).then(function (response) {
                alert(response.data);
               // $scope.isDisabled = true;
                $scope.btsu = false
                GetCartDetails();
            });
        }
        else {

        }
    }

});


//show membar our details
var showItem = angular.module('MemDetail', []);
showItem.controller('MemDetailController', function ($scope, $http, $window) {
    GetMembarDetails();
    GetMembarRequrment();
    function GetMembarDetails() {
        $http.get("/Secretary/GetMembar/").then(function (response) {


            $scope.Membar = response.data;
            // alert($scope.Car);

        }, function () {
            alert('Data not found');
        });
    }
    function GetMembarRequrment() {
        $http.get("/Secretary/GetMembarRqurmet/").then(function (response) {


            $scope.Membars = response.data;
            // alert($scope.Car);

        }, function () {
            alert('Data not found');
        });
    }
});


var Rep = angular.module('Report', []);
Rep.controller('ReportDetalisController', function ($scope, $http, $window) {

   $scope.Report = function () {
 //      alert($scope.MenuRep)
    //    alert($scope.sdate)
        
       if ($scope.MenuRep == "1") {
           $http({
               url: "/Secretary/ReportSell/",
               method: "POST",
               params: { sd: $scope.sdate, ld: $scope.ldate, tp: $scope.MenuRep }

           }).then(function (response) {
               if (response.data == "success") {
                   window.open('/Secretary/Print', '_blank');
               } else {
                   alert(response.data)
               }


           });
       } else if ($scope.MenuRep == "Requirement") {
           $http({
               url: "/Secretary/ReportRequrment/",
               method: "POST",
               params: { sd: $scope.sdate, ld: $scope.ldate, tp: $scope.MenuRep }

           }).then(function (response) {
               if (response.data == "success") {
                   window.open('/Secretary/PrintRequ', '_blank');
               } else {
                   alert(response.data)
               }


           });

       } else if ($scope.MenuRep == "2") {

           $http({
               url: "/Secretary/ReportSellItem/",
               method: "POST",
               params: { sd: $scope.sdate, ld: $scope.ldate, tp: $scope.MenuRep }

           }).then(function (response) {
               if (response.data == "success") {
                   window.open('/Secretary/PrintItemSell', '_blank');
                   
               } else {
                   alert(response.data)
               }


           });

       } else {
           $http({
               url: "/Secretary/ReportRequrment/",
               method: "POST",
               params: { sd: $scope.sdate, ld: $scope.ldate, tp: $scope.MenuRep }

           }).then(function (response) {
               if (response.data == "success") {
                   window.open('/Secretary/PrintRequ', '_blank');
               } else {
                   alert(response.data)
               }


           });
       }
        


   }

    

});


var Pro = angular.module('ProUser', []);
Pro.controller('ProUserController', function ($scope, $http, $window) {
    $scope.Ptemail = "([_a-zA-Z0-9-]+)(\.[_a-zA-Z0-9-]+)*@([a-zA-Z]+\.)+([a-zA-Z]{2,3})";
    $scope.ProfileUser = function () {
        $http.get("/Home/GetProfile/").then(function (response) {


            $scope.Registration = response.data;


        }, function () {
            alert('Data not found');
        });
    }

    $scope.ProSave = function () {
      
            $http({
                url: "/Home/ProUpdate/",
                method: "POST",
                data: $scope.Registration
            }).then(function (response) {
                alert(response.data);
                window.location = "/Home/Index";

            });

        
    }
    $scope.SendEmail = function () {

        $http({
            url: "/Home/SendEmail/",
            method: "POST",
            params: { em: $scope.email }
        }).then(function (response) {
            alert(response.data);
            $scope.email = null;
          //  window.location = "/Home/Index";

        });


    }


});

//add crop for admin
var crop = angular.module('AddCrop', []);
crop.controller('AddCropController', function ($scope, $http, $window) {
    // $scope.validext = "([^\\s]+(\\.(?i)(jpg|png|gif|bmp))$)";
    CropGet();
    $scope.btn = "Save";

    $scope.init = function () {
        $http.get("/Home/GetItemTypeReg/").then(function (response) {
            $scope.Itypes = response.data;

        });
    }

    $scope.AddCrrop = function () {
      //  alert($scope.ItemMasert.ItypeId)
        var getAction = $scope.Action;
        if (getAction != "Update") {
            $scope.AddCrop.ItypeId = $scope.ItemMasert.ItypeId;
            $http({
                method: 'POST',
                url: '/Home/AddCropFer/',

                data: JSON.stringify($scope.AddCrop),
                dataType: "json"


            }).then(function (response) {
                alert(response.data)
                $scope.AddCrop = null;
                CropGet();
            });
        } else {
          //  alert(getAction)
            //alert("updater");
            $scope.spin = {};
            $scope.spin.kk = true;
            $scope.AddCrop.ItypeId = $scope.ItemMasert.ItypeId;
            $scope.btn = "Please wait...!";
            //update code
            $http({
                method: "post",
                url: "/Home/EditeCrop/",
                data: JSON.stringify($scope.AddCrop),
                dataType: "json"
            }).then(function (respons) {
                alert(respons.data);
                $scope.ItemType = null;
                $scope.spin.kk = false;
                $scope.btn = "Save";
                $scope.Action = null;
                $scope.AddCrop = null;
                CropGet();

            });


        }
    }


    //get Itemtype from itemtypetable
    function CropGet() {

        $http.get("/Home/GetCrop/").then(function (response) {
            $scope.GetCrop = response.data;
        }, function () {
            alert('Data not found');
        });

    }

    //delete code but stsus is 1 or 0 set for crop
    $scope.deletteCrop = function (ab) {

        if ($window.confirm("Do you want to Delete Record?")) {
            $http({
                url: "/Home/DeleteCrop/",
                method: "POST",
                params: { id: ab.cpid }
            }).then(function (response) {
                alert(response.data);

                CropGet();
            });

        }
    }


    $scope.getCrop = function (Re) {
        $scope.AddCrop = {};
      //  alert(Re.cpname)
        $scope.ItemMasert.ItypeId = Re.ItypeId;
        $scope.AddCrop.cpname = Re.cpname,
        $scope.AddCrop.cpid = Re.cpid,


            $scope.Action = "Update";
        $scope.btn = "Update";
    }
    //crop name exit or not
    $scope.CropCheck = function () {
        // alert($scope.ItemType.type)

        $http({
            url: "/Home/CropNameExit/",
            method: "GET",
            params: { nm: $scope.AddCrop.cpname, id: $scope.ItemMasert.ItypeId }
        }).then(function (response) {
            $scope.typeIm = response.data;


        });

    }
});