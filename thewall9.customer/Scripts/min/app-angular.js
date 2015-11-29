"use strict";var app=angular.module("app",["ngRoute","LocalStorageModule","blockUI","ui.tree","angularFileUpload","ui.tinymce","ui.bootstrap"]);app.config(["$routeProvider","blockUIConfig",function(a,b){a.when("/",{redirectTo:"/content/edit2"}),a.when("/configuration",{controller:"configurationController",templateUrl:"/app/views/configuration.html"}),a.when("/pages",{controller:"pagesController",templateUrl:"/app/views/pages.html"}),a.when("/page/edit/:pageID",{controller:"pageDetailController",templateUrl:"/app/views/page-detail.html"}),a.when("/content",{controller:"contentController",templateUrl:"/app/views/content.html"}),a.when("/password",{controller:"passwordController",templateUrl:"/app/views/password.html"}),a.when("/content/edit",{controller:"contentEditController",templateUrl:"/app/views/content-edit.html"}),a.when("/content/edit2/:ContentPropertyID?",{controller:"contentEditController2",templateUrl:"/app/views/content-edit2.html"}),a.when("/categories",{controller:"categoryController",templateUrl:"/app/views/categories.html"}),a.when("/currencies",{controller:"currencyController",templateUrl:"/app/views/currencies.html"}),a.when("/tags",{controller:"tagController",templateUrl:"/app/views/tags.html"}),a.when("/products",{controller:"productsController",templateUrl:"/app/views/products.html"}),a.when("/product/:productID?",{controller:"productController",templateUrl:"/app/views/product.html"}),a.when("/orders",{controller:"ordersController",templateUrl:"/app/views/orders.html"}),a.when("/blog",{controller:"blogController",templateUrl:"/app/views/blog.html"}),a.when("/blog/post/:blogPostID?",{controller:"blogPostController",templateUrl:"/app/views/blogPost.html"}),a.when("/blog/categories?",{controller:"blogCategoryController",templateUrl:"/app/views/blogCategory.html"}),a.otherwise({redirectTo:"/"}),b.message="Cargando...",b.delay=100,b.autoBlock=!1}]),app.constant("ngAuthSettings",{apiServiceBaseUri:serviceBase,clientId:"ngAuthApp"}),app.config(["$httpProvider",function(a){a.interceptors.push("authInterceptorService")}]),app.run(["authService",function(a){a.fillAuthData()}]),app.filter("fromNow",function(){return function(a){return moment.utc(a).utcOffset((new Date).getTimezoneOffset()).locale("es").fromNow()}}),app.factory("authInterceptorService",["$q","$injector","$location","localStorageService","blockUI",function(a,b,c,d,e){var f={};return f.request=function(a){null==e.noOpen&&e.start(),a.headers=a.headers||{};var b=d.get("authorizationData");return b&&(a.headers.Authorization="Bearer "+b.token),a},f.responseError=function(b){return null==e.noOpen?e.stop():e.noOpen=null,a.reject(b)},f.response=function(b){return null==e.noOpen?e.stop():e.noOpen=null,b||a.when(b)},f}]),app.factory("authService",["$http","$q","localStorageService","ngAuthSettings","utilService",function(a,b,c,d,e){var f=d.apiServiceBaseUri,g={},h={isAuth:!1,userName:"",useRefreshTokens:!1},i={provider:"",userName:"",externalAccessToken:""},j=function(b){return l(),a.post(f+"api/account/register",b).then(function(a){return a})},k=function(e){var g="grant_type=password&username="+e.userName+"&password="+e.password;g=g+"&client_id="+d.clientId;var i=b.defer();return a.post(f+"token",g,{headers:{"Content-Type":"application/x-www-form-urlencoded"}}).success(function(a){c.set("authorizationData",{token:a.access_token,userName:e.userName,refreshToken:a.refresh_token,useRefreshTokens:!0}),h.isAuth=!0,h.userName=e.userName,h.useRefreshTokens=e.useRefreshTokens,i.resolve(a)}).error(function(a,b){l(!0),i.reject(a)}),i.promise},l=function(a){c.remove("authorizationData"),h.isAuth=!1,h.userName="",h.useRefreshTokens=!1,null==a&&(window.location="/")},m=function(){var a=c.get("authorizationData");a&&(h.isAuth=!0,h.userName=a.userName,h.useRefreshTokens=a.useRefreshTokens)},n=function(){var e=b.defer(),g=c.get("authorizationData");if(g&&g.useRefreshTokens){var h="grant_type=refresh_token&refresh_token="+g.refreshToken+"&client_id="+d.clientId;c.remove("authorizationData"),a.post(f+"token",h,{headers:{"Content-Type":"application/x-www-form-urlencoded"}}).success(function(a){c.set("authorizationData",{token:a.access_token,userName:a.userName,refreshToken:a.refresh_token,useRefreshTokens:!0}),e.resolve(a)}).error(function(a,b){l(),e.reject(a)})}return e.promise},o=function(d){var e=b.defer();return a.get(f+"api/account/ObtainLocalAccessToken",{params:{provider:d.provider,externalAccessToken:d.externalAccessToken}}).success(function(a){c.set("authorizationData",{token:a.access_token,userName:a.userName,refreshToken:"",useRefreshTokens:!1}),h.isAuth=!0,h.userName=a.userName,h.useRefreshTokens=!1,e.resolve(a)}).error(function(a,b){l(),e.reject(a)}),e.promise},p=function(d){var e=b.defer();return a.post(f+"api/account/registerexternal",d).success(function(a){c.set("authorizationData",{token:a.access_token,userName:a.userName,refreshToken:"",useRefreshTokens:!1}),h.isAuth=!0,h.userName=a.userName,h.useRefreshTokens=!1,e.resolve(a)}).error(function(a,b){l(),e.reject(a)}),e.promise};return g.saveRegistration=j,g.login=k,g.logOut=l,g.fillAuthData=m,g.authentication=h,g.refreshToken=n,g.obtainAccessToken=o,g.externalAuthData=i,g.registerExternal=p,g.changePassword=function(b){return a.post(f+"api/account/changepassword",b)},g.roles=[],g.isAdmin=!1,g.getRoles=function(){var c=b.defer();return a.get(f+"api/account/roles").success(function(a){g.roles=a,angular.forEach(a,function(a){"admin"==a&&(g.isAdmin=!0)}),c.resolve(a)}).error(e.errorCallback),c.promise},g}]),app.factory("blogService",["$myhttp","$q","ngAuthSettings","siteService","cultureService",function(a,b,c,d,e){var f="api/blog",g={};return g.get=function(){return a.get(c.apiServiceBaseUri+f+"?SiteID="+d.site.SiteID+"&CultureID="+e.currentCulture.CultureID)},g.getByID=function(b){return a.get(c.apiServiceBaseUri+f+"/byID?BlogPostID="+b+"&CultureID="+e.currentCulture.CultureID)},g.save=function(b){return b.SiteID=d.site.SiteID,b.CultureID=e.currentCulture.CultureID,a.post(c.apiServiceBaseUri+f,b)},g.remove=function(b){return a["delete"](c.apiServiceBaseUri+f+"?BlogPostID="+b)},g.getCategories=function(){return a.get(c.apiServiceBaseUri+f+"/category?SiteID="+d.site.SiteID+"&CultureID="+e.currentCulture.CultureID)},g.editCategory=function(a,b){null==a?b.itemToSave={CategoryCultures:[]}:b.itemToSave=a,angular.forEach(e.cultures,function(a){var c=!1;angular.forEach(b.itemToSave.CategoryCultures,function(b){a.CultureID==b.CultureID&&(c=!0)}),c||b.itemToSave.CategoryCultures.push({CultureID:a.CultureID,CultureName:a.Name})}),$("#modal-new").modal({backdrop:!0})},g.saveCategory=function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+f+"/category",b)},g.getTags=function(b){return a.get(c.apiServiceBaseUri+f+"/tag?Query="+b)},g}]),app.factory("categoryService",["$myhttp","$q","ngAuthSettings","siteService",function(a,b,c,d){var e="api/category",f={};return f.get=function(){return a.get(c.apiServiceBaseUri+e+"?SiteID="+d.site.SiteID)},f.save=function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+e,b)},f.up=function(b,d){return a.post(c.apiServiceBaseUri+e+"/up-or-down",{CategoryID:b,Up:d})},f.remove=function(b){return a["delete"](c.apiServiceBaseUri+e+"?CategoryID="+b)},f}]),app.factory("contentService",["$myhttp","$q","ngAuthSettings","siteService","cultureService",function(a,b,c,d,e){return{get:function(){return a.get(c.apiServiceBaseUri+"api/content?SiteID="+d.site.SiteID)},getMenu:function(){return a.get(c.apiServiceBaseUri+"api/content/menu?SiteID="+d.site.SiteID+"&CultureID="+e.currentCulture.CultureID)},getTree:function(){return a.get(c.apiServiceBaseUri+"api/content?SiteID="+d.site.SiteID+"&CultureID="+e.currentCulture.CultureID)},getTreeByProperty:function(b){return a.get(c.apiServiceBaseUri+"api/content/property?ContentPropertyID="+b+"&CultureID="+e.currentCulture.CultureID)},save:function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+"api/content",b)},saveTree:function(b){var f={SiteID:d.site.SiteID,CultureID:e.currentCulture.CultureID,Items:b};return a.post(c.apiServiceBaseUri+"api/content/save-tree",f)},remove:function(b){return a["delete"](c.apiServiceBaseUri+"api/content?ContentPropertyID="+b.ContentPropertyID)},move:function(b,d,e){return a.post(c.apiServiceBaseUri+"api/content/move",{Index:b,ContentPropertyParentID:d,ContentPropertyID:e})},duplicate:function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+"api/content/duplicate",b)},duplicateTree:function(b){return b.SiteID=d.site.SiteID,b.CultureID=e.currentCulture.CultureID,a.post(c.apiServiceBaseUri+"api/content/duplicate-tree",b)},exportContent:function(b){return a.get(c.apiServiceBaseUri+"api/content/export?SiteID="+d.site.SiteID+"&ContentPropertyID="+b)},importContent:function(b,e){var f={SiteID:d.site.SiteID,ContentPropertyID:b,FileRead:e};return a.post(c.apiServiceBaseUri+"api/content/import",f)},lock:function(b,d){return a.post(c.apiServiceBaseUri+"api/content/lock",{ContentPropertyID:b,Boolean:d})},enable:function(b,d){return a.post(c.apiServiceBaseUri+"api/content/enable",{ContentPropertyID:b,Boolean:d})},lockAll:function(){return a.post(c.apiServiceBaseUri+"api/content/lock-all",d.site.SiteID)},showInContent:function(b,d){return a.post(c.apiServiceBaseUri+"api/content/show-in-content",{ContentPropertyID:b,Boolean:d})},inMenu:function(b,d){return a.post(c.apiServiceBaseUri+"api/content/inmenu",{ContentPropertyID:b,Boolean:d})}}}]),app.factory("cultureService",["$myhttp","$q","ngAuthSettings","siteService",function(a,b,c,d){var e={};return e.cultures=[],e.currentCulture={},e.get=function(){var f=b.defer();return a.get(c.apiServiceBaseUri+"api/culture?SiteID="+d.site.SiteID).then(function(a){e.cultures=a,e.currentCulture=a[0],f.resolve(a)})},e.save=function(b){return a.post(c.apiServiceBaseUri+"api/culture",b)},e}]),app.factory("currencyService",["$myhttp","$q","ngAuthSettings","siteService",function(a,b,c,d){var e="api/currency",f={};return f.get=function(){return a.get(c.apiServiceBaseUri+e+"?SiteID="+d.site.SiteID)},f.save=function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+e,b)},f["default"]=function(b){return a.put(c.apiServiceBaseUri+e+"/default?CurrencyID="+b)},f.remove=function(b){return a["delete"](c.apiServiceBaseUri+e+"?CurrencyID="+b)},f}]),app.factory("mediaService",["$myhttp","$q","ngAuthSettings","siteService",function(a,b,c,d){var e="api/media",f={};return f.upload=function(b){return a.post(c.apiServiceBaseUri+e,b)},f}]),app.factory("$myhttp",["$http","$q","utilService","authService","localStorageService",function(a,b,c,d,e){var f={};return f.get=function(g,h){var i=b.defer();return a.get(g).success(function(a){i.resolve(a)}).error(function(a,b){if(401===b){var h=e.get("authorizationData");h?h.useRefreshTokens&&d.refreshToken().then(function(){f.get(g).then(function(a){i.resolve(a)})}):d.logOut()}else c.errorCallback(a),i.reject(a)}),i.promise},f.post=function(g,h){var i=b.defer();return a.post(g,h).success(function(a){i.resolve(a)}).error(function(a,b){if(401===b){var j=e.get("authorizationData");j?j.useRefreshTokens&&d.refreshToken().then(function(){f.post(g,h).then(function(a){i.resolve(a)})}):d.logOut()}else c.errorCallback(a),i.reject(a)}),i.promise},f.put=function(g,h){var i=b.defer();return a.put(g,h).success(function(a){i.resolve(a)}).error(function(a,b){if(401===b){var j=e.get("authorizationData");j?j.useRefreshTokens&&d.refreshToken().then(function(){f.put(g,h).then(function(a){i.resolve(a)})}):d.logOut()}else c.errorCallback(a),i.reject(a)}),i.promise},f["delete"]=function(g){var h=b.defer();return a["delete"](g).success(function(a){h.resolve(a)}).error(function(a,b){if(401===b){var i=e.get("authorizationData");i?i.useRefreshTokens&&d.refreshToken().then(function(){f["delete"](g).then(function(a){h.resolve(a)})}):d.logOut()}else c.errorCallback(a),h.reject(a)}),h.promise},f}]),app.factory("orderService",["$myhttp","$q","ngAuthSettings","siteService",function(a,b,c,d){var e="api/order",f={};return f.get=function(){return a.get(c.apiServiceBaseUri+e+"?SiteID="+d.site.SiteID)},f.save=function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+e,b)},f.remove=function(b){return a["delete"](c.apiServiceBaseUri+e+"?OrderID="+b)},f}]),app.factory("pageService",["$myhttp","ngAuthSettings","siteService",function(a,b,c){return{get:function(){return a.get(b.apiServiceBaseUri+"api/page?SiteID="+c.site.SiteID)},getDetail:function(c,d){return a.get(b.apiServiceBaseUri+"api/page?PageID="+c+"&CultureID="+d)},save:function(d){return d.SiteID=c.site.SiteID,a.post(b.apiServiceBaseUri+"api/page",d)},saveCulture:function(d){return d.SiteID=c.site.SiteID,a.post(b.apiServiceBaseUri+"api/page/save-culture",d)},remove:function(c){return a["delete"](b.apiServiceBaseUri+"api/page?PageID="+c.PageID)},move:function(c,d,e){return a.post(b.apiServiceBaseUri+"api/page/move",{Index:c,PageParentID:d,PageID:e})},publish:function(c,d){return a.post(b.apiServiceBaseUri+"api/page/publish",{PageID:c,Published:d})},inMenu:function(c,d){return a.post(b.apiServiceBaseUri+"api/page/in-menu",{PageID:c,Published:d})}}}]),app.factory("productService",["$myhttp","$q","$upload","ngAuthSettings","siteService",function(a,b,c,d,e){var f="api/product",g="api/product-gallery",h={};return h.get=function(){return a.get(d.apiServiceBaseUri+f+"?SiteID="+e.site.SiteID)},h.getByID=function(b){return a.get(d.apiServiceBaseUri+f+"/byID?ProductID="+b)},h.save=function(b){return b.SiteID=e.site.SiteID,a.post(d.apiServiceBaseUri+f,b)},h.remove=function(b){return a["delete"](d.apiServiceBaseUri+f+"?ProductID="+b)},h.removeGallery=function(b){return a["delete"](d.apiServiceBaseUri+f+"/gallery?GalleryID="+b)},h.move=function(b,c){return a.post(d.apiServiceBaseUri+f+"/move",{ProductID:b,Index:c})},h.uploadGallery=function(a,b){return c.upload({url:d.apiServiceBaseUri+g+"/upload",fields:{ProductID:a},file:b})},h.getCategories=function(b){return a.get(d.apiServiceBaseUri+f+"/categories?SiteID="+e.site.SiteID+"&Query="+b)},h.getCategoriesUrl=function(){return d.apiServiceBaseUri+f+"/categories?SiteID="+e.site.SiteID+"&Query=%QUERY"},h.getTags=function(b){return a.get(d.apiServiceBaseUri+f+"/tags?SiteID="+e.site.SiteID+"&Query="+b)},h}]),app.factory("siteService",["$myhttp","$q","ngAuthSettings","localStorageService",function(a,b,c,d){var e={};return e.site={},e.sites=[],e.sitesLoaded=!1,e.getSites=function(){var f=b.defer();return a.get(c.apiServiceBaseUri+"api/site").then(function(a){var b=d.get("site"),c=!1;null!=b&&angular.forEach(a,function(a){a.SiteID==b.SiteID&&(c=!0)}),c?e.site=b:(e.site=a[0],b=e.site),e.sites=a,e.sitesLoaded=!0,d.set("site",e.site),f.resolve(a)}),f.promise},e.save=function(b){return a.put(c.apiServiceBaseUri+"api/site",b)},e.change=function(a){e.site=a,d.set("site",a)},e}]),app.factory("tagService",["$myhttp","$q","ngAuthSettings","siteService",function(a,b,c,d){var e="api/tag",f={};return f.get=function(){return a.get(c.apiServiceBaseUri+e+"?SiteID="+d.site.SiteID)},f.save=function(b){return b.SiteID=d.site.SiteID,a.post(c.apiServiceBaseUri+e,b)},f.remove=function(b){return a["delete"](c.apiServiceBaseUri+e+"?tagID="+b)},f}]),app.factory("toastrService",[function(){return{success:function(a){toastr.success(a)},error:function(a){if(null!=a.ModelState)for(var b in a.ModelState)for(var c=0;c<a.ModelState[b].length;c++)toastr.error(a.ModelState[b][c],"Error");else null!=a.Message?toastr.error(a.Message,"Error"):toastr.error(a,"Error")}}}]),app.factory("utilService",["toastrService",function(a){return{errorCallback:function(b){a.error(b)}}}]),app.controller("appController",["$scope","$rootScope","$q","$location","authService","siteService","cultureService",function(a,b,c,d,e,f,g){a.logOut=function(){e.logOut(),window.location="/"},a.authentication=e.authentication,f.getSites().then(function(b){0==f.sites.length?a.logOut():(a.site=f.site,a.sites=f.sites,a.updateSiteInfo())}),a.changeSite=function(b){f.change(b),a.site=f.site,a.updateSiteInfo()},a.updateSiteInfo=function(){e.getRoles().then(function(c){a.isAdmin=e.isAdmin,g.get().then(function(c){a.cultures=g.cultures,b.$broadcast("initDone")})})}}]),app.controller("blogCategoryController",["$scope","$routeParams","$location","blogService","siteService","toastrService","cultureService",function(a,b,c,d,e,f,g){a.getCategories=function(){return d.getCategories().then(function(b){a.data=b})},a.edit=function(b){d.editCategory(b,a)},a.updateCulture=function(){g.currentCulture=a.selectedCulture,a.init()},a.init=function(){a.selectedCulture=g.currentCulture,a.getCategories()},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&g.currentCulture.CultureID&&a.init()}]),app.controller("blogCategorySaveController",["$scope","$routeParams","$location","blogService","siteService","toastrService","cultureService",function(a,b,c,d,e,f,g){a.save=function(){d.saveCategory(a.itemToSave).then(function(b){f.success("Post Guardado"),$("#modal-new").modal("hide"),a.getCategories()})},a.updateCulture=function(){g.currentCulture=a.selectedCulture,a.init()},a.init=function(){a.selectedCulture=g.currentCulture},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&a.init()}]),app.controller("blogController",["$scope","$routeParams","$location","blogService","siteService","toastrService","cultureService",function(a,b,c,d,e,f,g){a.get=function(){d.get().then(function(b){a.data=b})},a.remove=function(b){confirm("¿Desea Eliminar el Post?")&&d.remove(b.BlogPostID).then(function(b){a.get()})},a.updateCulture=function(){g.currentCulture=a.selectedCulture,a.init()},a.init=function(){a.selectedCulture=g.currentCulture,a.get()},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&g.currentCulture.CultureID&&a.init()}]),app.controller("blogPostController",["$scope","$routeParams","$location","blogService","siteService","toastrService","cultureService","blockUI",function(a,b,c,d,e,f,g,h){a.tinymceOptions={plugins:["advlist autolink lists link image charmap print preview anchor","searchreplace visualblocks code fullscreen","insertdatetime media table contextmenu paste"],toolbar:"insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image",extended_valid_elements:"iframe[src|width|height|name|align]",format:null},a.blogPostID=b.blogPostID,a.get=function(){null!=a.blogPostID?d.getByID(a.blogPostID).then(function(b){a.model=b,a.mediaList=b.ImagesFileRead,angular.forEach(a.model.Categories,function(b){angular.forEach(a.categories,function(a){a.BlogCategoryID==b.BlogCategoryID&&(a.Enabled=!0)})})}):a.model={Tags:[]}},a.save=function(b){a.model.Published=b,d.save(a.model).then(function(b){f.success("Post Guardado"),null==a.blogPostID?c.path("/blog/post/"+b):a.init()})},a.getCategories=function(){return d.getCategories().then(function(b){a.categories=b,a.get()})},a.selectCategory=function(b){var c=null;angular.forEach(a.model.Categories,function(a){a.BlogCategoryID==b.BlogCategoryID&&(b.Enabled=!b.Enabled,c=a,c.Deleting=!b.Enabled,c.Adding=b.Enabled)}),null==c&&(b.Enabled=!0,a.model.Categories.push({BlogCategoryID:b.BlogCategoryID,Adding:!0,Deleting:!1}))},a.getTags=function(a){return h.noOpen=!0,d.getTags(a)},a.updateCulture=function(){g.currentCulture=a.selectedCulture,a.init()},a.init=function(){a.selectedCulture=g.currentCulture,a.getCategories()},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&g.currentCulture.CultureID&&a.init()}]),app.controller("categoryController",["$scope","categoryService","siteService","toastrService",function(a,b,c,d){function e(b,c){angular.forEach(b,function(b){a.categories.push(angular.copy(b)),e(b.CategoryItems)})}a.get=function(){b.get().then(function(b){a.categories=[],e(b),a.data=b})},a.openNew=function(b){a.selectedCategory={},null!=b&&angular.forEach(a.categories,function(c){c.CategoryID==b.CategoryID&&(a.selectedCategory=c)}),a.model={CategoryParentID:a.selectedCategory.CategoryID,CategoryCultures:[]},angular.forEach(a.cultures,function(b){a.model.CategoryCultures.push({CultureID:b.CultureID,CultureName:b.Name,CategoryName:""})}),$("#modal-new").modal({backdrop:!0})},a.edit=function(b){a.selectedCategory={},null!=b&&angular.forEach(a.categories,function(c){c.CategoryID==b.CategoryParentID&&(a.selectedCategory=c)}),a.model=b,angular.forEach(a.cultures,function(c){var d=!1;angular.forEach(a.model.CategoryCultures,function(a){a.CultureID==c.CultureID&&(d=!0)}),d||a.model.CategoryCultures.push({CultureID:c.CultureID,CultureName:c.Name,CategoryName:"",CategoryID:b.CategoryID,Adding:!0,FriendlyUrl:b.FriendlyUrl})}),$("#modal-new").modal({backdrop:!0})},a.save=function(){b.save(a.model).then(function(b){a.get()}),$("#modal-new").modal("hide")},a["delete"]=function(c){confirm("¿Estas seguro que deseas eliminar esta categoría?")&&b.remove(c.CategoryID).then(function(b){a.get(),d.success("Categoría Eliminada")})},a.up=function(c,d){b.up(c.CategoryID,d).then(function(b){a.get()})},a.init=function(){a.get()},a.$on("initDone",function(b){a.init()}),c.sitesLoaded&&a.init()}]),app.controller("configurationController",["$scope","siteService","cultureService","authService","toastrService",function(a,b,c,d,e){a.saveSite=function(){b.save(a.site).then(function(a){e.success("Configuración Guardada")})},a.save=function(a){c.save(a).then(function(a){e.success("Configuración Guardada")})},a.init=function(){a.site=b.site},a.$on("initDone",function(b){a.init()}),b.sitesLoaded&&a.init()}]),app.controller("contentController",["$scope","toastrService","contentService","cultureService","siteService",function(a,b,c,d,e){a.properties=[{TypeName:"IMG",TypeID:1},{TypeName:"TXT",TypeID:2},{TypeName:"LIST",TypeID:3},{TypeName:"HTML",TypeID:4}],a.fileImport={},a.selectedItem={},a.get=function(){a.cultures=d.cultures,c.get().then(function(b){a.data=b})},a.options={dropped:function(a){var d=a.source.nodeScope,e=a.dest.nodesScope,f=a.dest.index,g=null!=e.$nodeScope?e.$nodeScope.$modelValue.ContentPropertyID:0;c.move(f,g,d.$modelValue.ContentPropertyID).then(function(a){b.success("Propiedad movida exitosamente")})}},a.removeContent=function(a){confirm("¿Estás seguro que quieres eliminar esta propiedad? también se eliminarán las propiedades hijas")&&c.remove(a.$modelValue).then(function(c){a.remove(),b.success("Propiedad y propiedades hijas eliminadas")})},a.toggle=function(a){a.toggle()},a.openNew=function(b){a.content={ContentPropertyTypeOptions:a.properties[2]},null!=b?(a.contentParent=b.$modelValue,a.content.ContentPropertyParentID=a.contentParent.ContentPropertyID):a.contentParent=null,$("#modal-new").modal({backdrop:!0})},a.edit=function(b){a.content=b,angular.forEach(a.properties,function(b){b.TypeID==a.content.ContentPropertyType&&(a.content.ContentPropertyTypeOptions=b)}),$("#modal-new").modal({backdrop:!0})},a.create=function(){a.content.ContentPropertyType=a.content.ContentPropertyTypeOptions.TypeID,c.save(a.content).then(function(c){if(null==a.content.ContentPropertyID)if(null==a.contentParent)a.data.push({ContentPropertyID:c,ContentPropertyAlias:a.content.ContentPropertyAlias,ContentPropertyParentID:0,ContentPropertyType:a.content.ContentPropertyType,Items:[],Priority:a.data[a.data.length-1].Priority+1});else{var d=0;0!=a.contentParent.Items.length&&(d=a.contentParent.Items[a.contentParent.Items.length-1].Priority+1),a.contentParent.Items.push({ContentPropertyID:c,ContentPropertyAlias:a.content.ContentPropertyAlias,ContentPropertyParentID:a.contentParent.ContentPropertyID,ContentPropertyType:a.content.ContentPropertyType,Items:[],Priority:d})}$("#modal-new").modal("hide"),b.success("Propiedad Creada")})},a.duplicate=function(d,e){var f=a.data;null!=d.$parentNodeScope&&(f=d.$parentNodeScope.$modelValue.Items),c.duplicate(e).then(function(a){f.push(a),b.success("Propiedades duplicadas")})},a.fileImport={},a["export"]=function(a){c.exportContent(null==a?0:a.ContentPropertyID).then(function(a){window.open(a.Url,"_blank","")})},a["import"]=function(d){c.importContent(null==d?0:d.ContentPropertyID,d.FileImport).then(function(c){null==d?a.fileImport=null:null==d.FileImport,b.success("Propiedades creadas exitosamente, cargando nuevas propiedades.."),a.get()})},a.lock=function(a,b){c.lock(a.ContentPropertyID,b).then(function(c){a.Lock=b})},a.lockAll=function(){c.lockAll().then(function(b){a.get()})},a.showInContent=function(a,b){c.showInContent(a.ContentPropertyID,b).then(function(c){a.ShowInContent=b})},a.inMenu=function(a,b){c.inMenu(a.ContentPropertyID,b).then(function(c){a.InMenu=b})},a.init=function(){a.get()},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&a.init()}]),app.controller("contentEditController",["$scope","$location","toastrService","contentService","cultureService","siteService",function(a,b,c,d,e,f){function g(a,b){angular.forEach(b,function(b){b.ContentPropertyID==a.ContentPropertyParentID?b.Items.push(a):null!=b.Items&&b.Items.length>0&&g(a,b.Items)})}$(window).scroll(function(){$(this).scrollTop()>100?a.$apply(function(){a.showItemsFixed=!0}):a.$apply(function(){a.showItemsFixed=!1})});var h=b.search().isAdmin;null!=h&&h?a.isAdmin=!0:a.isAdmin=!1,a.get=function(){d.getTree().then(function(b){a.data=b})},a.save=function(){d.saveTree(a.data).then(function(a){c.success("Cambios guardados exitosamente")})},a.duplicate=function(b){d.duplicateTree(b).then(function(d){console.log("DUPLICATING",d),g(d,a.data),c.success("Nuevo "+b.Hint+" creado")})},a["delete"]=function(b){confirm("¿Estás seguro que quieres eliminar esta propiedad?")&&d.remove(b).then(function(b){a.get(),c.success("Propiedad eliminada")})},a.updateCulture=function(){e.currentCulture=a.selectedCulture,a.init()},a.enable=function(a,b){d.enable(a.ContentPropertyID,b).then(function(c){a.Enabled=b})},a.upDown=function(b,e){console.log("prio",b.Priority);var f=e?b.Priority-1:b.Priority+1;console.log("INDEX",f),f>=0&&d.move(f,b.ContentPropertyParentID,b.ContentPropertyID).then(function(b){a.get(),c.success("Propiedad movida exitosamente")})},a.init=function(){a.selectedCulture=e.currentCulture,a.selectedCulture.CultureID&&a.get()},a.$on("initDone",function(b){a.init()}),f.sitesLoaded&&a.init()}]),app.controller("contentEditController2",["$scope","$routeParams","$location","toastrService","contentService","cultureService","siteService",function(a,b,c,d,e,f,g){function h(a,b){angular.forEach(b,function(b){b.ContentPropertyID==a.ContentPropertyParentID?b.Items.push(a):null!=b.Items&&b.Items.length>0&&h(a,b.Items)})}$(window).scroll(function(){$(this).scrollTop()>100?a.$apply(function(){a.showItemsFixed=!0}):a.$apply(function(){a.showItemsFixed=!1})});var i=c.search().isAdmin;null!=i&&i?a.isAdmin=!0:a.isAdmin=!1,a.getMenu=function(){null==a.menu?e.getMenu().then(function(c){a.menu=c;var d=null==b.ContentPropertyID?a.menu[0].ContentPropertyID:b.ContentPropertyID;a.get(d)}):a.get(b.ContentPropertyID)},a.get=function(b){null==b&&(b=a.menuSelected.ContentPropertyID),angular.forEach(a.menu,function(c){c.ContentPropertyID==b?(c.Selected=!0,c.Items=[],a.menuSelected=c):c.Selected=!1}),e.getTreeByProperty(b).then(function(b){a.data=b})},a.save=function(){e.saveTree(a.data).then(function(b){a.menuSelected.Edit?e.saveTree([a.menuSelected]).then(function(a){d.success("Cambios guardados exitosamente")}):d.success("Cambios guardados exitosamente")})},a.duplicate=function(b){e.duplicateTree(b).then(function(c){console.log("DUPLICATING",c),h(c,a.data),d.success("Nuevo "+b.Hint+" creado")})},a["delete"]=function(b){confirm("¿Estás seguro que quieres eliminar esta propiedad?")&&e.remove(b).then(function(b){a.get(),d.success("Propiedad eliminada")})},a.enable=function(a,b){e.enable(a.ContentPropertyID,b).then(function(c){a.Enabled=b})},a.upDown=function(b,c){console.log("prio",b.Priority);var f=c?b.Priority-1:b.Priority+1;console.log("INDEX",f),f>=0&&e.move(f,b.ContentPropertyParentID,b.ContentPropertyID).then(function(b){a.get(),d.success("Propiedad movida exitosamente")})},a.updateCulture=function(){f.currentCulture=a.selectedCulture,a.init()},a.init=function(){a.selectedCulture=f.currentCulture,a.menu=null,a.selectedCulture.CultureID&&a.getMenu()},a.$on("initDone",function(b){a.init()}),g.sitesLoaded&&a.init()}]),app.controller("currencyController",["$scope","currencyService","siteService","toastrService",function(a,b,c,d){a.get=function(){b.get().then(function(b){a.data=b})},a.open=function(b){null==b?a.model={}:a.model=angular.copy(b),$("#modal-new").modal({backdrop:!0})},a.save=function(){b.save(a.model).then(function(b){a.get()}),$("#modal-new").modal("hide")},a["delete"]=function(c){confirm("¿Estas seguro que deseas eliminar esta moneda?")&&b.remove(c.CurrencyID).then(function(b){a.get(),d.success("Categoría Eliminada")})},a["default"]=function(c){b["default"](c.CurrencyID).then(function(b){a.get()})},a.init=function(){a.get()},a.$on("initDone",function(b){a.init()}),c.sitesLoaded&&a.init()}]),app.controller("homeController",["$scope",function(a){}]),app.controller("loginController",["$scope","$location","authService","ngAuthSettings",function(a,b,c,d){c.authentication.isAuth&&(window.location="/intern"),a.loginData={userName:"",password:"",useRefreshTokens:!1},a.message="",a.login=function(){a.$broadcast("autofill:update"),c.login(a.loginData).then(function(a){window.location="/intern"},function(b){a.message=b.error_description})},a.authExternalProvider=function(b){var c=location.protocol+"//"+location.host+"/authcomplete.html",e=d.apiServiceBaseUri+"api/Account/ExternalLogin?provider="+b+"&response_type=token&client_id="+d.clientId+"&redirect_uri="+c;window.$windowScope=a;window.open(e,"Authenticate Account","location=0,status=0,width=600,height=750")},a.authCompletedCB=function(d){a.$apply(function(){if("False"==d.haslocalaccount)c.logOut(),c.externalAuthData={provider:d.provider,userName:d.external_user_name,externalAccessToken:d.external_access_token},b.path("/associate");else{var e={provider:d.provider,externalAccessToken:d.external_access_token};c.obtainAccessToken(e).then(function(a){b.path("/orders")},function(b){a.message=b.error_description})}})}}]),app.controller("mediaListController",["$scope","$routeParams","$location","siteService","toastrService","mediaService",function(a,b,c,d,e,f){a.removeMedia=function(a){a.Deleting=!0,a.Adding=!1},a.addMedia=function(){a.mediaModel.SiteID=d.site.SiteID,f.upload(a.mediaModel).then(function(b){b.Adding=!0,a.mediaList.push(b)})}}]),app.controller("ordersController",["$scope","orderService","siteService","toastrService",function(a,b,c,d){a.get=function(){b.get().then(function(b){a.data=b})},a.open=function(b){null==b?a.model={}:a.model=angular.copy(b),$("#modal-new").modal({backdrop:!0})},a["delete"]=function(c){confirm("¿Estas seguro que deseas eliminar esta Orden?")&&b.remove(c.OrderID).then(function(b){a.get(),d.success("Orden Eliminada")})},a.init=function(){a.get()},a.$on("initDone",function(b){a.init()}),c.sitesLoaded&&a.init()}]),app.controller("pageDetailController",["$scope","$routeParams","toastrService","pageService","cultureService","siteService",function(a,b,c,d,e,f){a.metatagDescriptionMin=150,a.metatagDescriptionMax=160,a.save=function(){d.saveCulture(a.page).then(function(a){c.success("Página Guardada")})},a.updateCulture=function(){e.currentCulture=a.selectedCulture,a.init()},a.init=function(){a.selectedCulture=e.currentCulture,a.selectedCulture.CultureID&&d.getDetail(b.pageID,e.currentCulture.CultureID).then(function(b){a.page=b})},a.$on("initDone",function(b){a.init()}),f.sitesLoaded&&a.init()}]),app.controller("pagesController",["$scope","toastrService","pageService","siteService",function(a,b,c,d){a.init=function(){c.get().then(function(b){a.data=b})},a.$on("initDone",function(b){a.init()}),d.sitesLoaded&&a.init(),a.selectedItem={},a.options={dropped:function(a){var d=a.source.nodeScope,e=a.dest.nodesScope,f=a.dest.index,g=null!=e.$nodeScope?e.$nodeScope.$modelValue.PageID:0;c.move(f,g,d.$modelValue.PageID).then(function(a){b.success("Página movida exitosamente")})}},a.removePage=function(a){confirm("¿Estás seguro que quieres eliminar esta sección? también se eliminarán las secciones hijas")&&c.remove(a.$modelValue).then(function(c){
a.remove(),b.success("Página y páginas hijas eliminados")})},a.toggle=function(a){a.toggle()},a.openNew=function(b){a.page={},null!=b?(a.pageParent=b.$modelValue,a.page.PageParentID=a.pageParent.PageID,a.page.PageParentAlias=a.pageParent.Alias):a.pageParent=null,$("#modal-new").modal({backdrop:!0})},a.createPage=function(){c.save(a.page).then(function(c){null==a.pageParent?a.data.push({PageID:c,Alias:a.page.Alias,PageParentID:a.page.PageParentID,Items:[]}):a.pageParent.Items.push({PageID:c,Alias:a.page.Alias,PageParentID:a.page.PageParentID,Items:[]}),$("#modal-new").modal("hide"),b.success("Página Creada")})},a.publish=function(a,b){c.publish(a.PageID,b).then(function(c){a.Published=b})},a.inMenu=function(a,b){c.inMenu(a.PageID,b).then(function(c){a.InMenu=b})}}]),app.controller("passwordController",["$scope","$rootScope","authService","toastrService","siteService",function(a,b,c,d,e){a.change=function(){c.changePassword(a.p).then(function(a){d.success("Clave Cambiada")})},a.init=function(){},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&a.init()}]),app.controller("productController",["$scope","$routeParams","$location","productService","siteService","currencyService","toastrService",function(a,b,c,d,e,f,g){a.get=function(){a.model={ProductCultures:[],ProductCategories:[],ProductCurrencies:[],ProductTags:[]},null!=b.productID?d.getByID(b.productID).then(function(b){a.model=b,angular.forEach(a.cultures,function(c){var d=!1;angular.forEach(a.model.ProductCultures,function(a){a.CultureID==c.CultureID&&(d=!0)}),d||a.model.ProductCultures.push({CultureID:c.CultureID,CultureName:c.Name,ProductName:"",ProductID:b.ProductID,Adding:!0})}),angular.forEach(a.currencies,function(c){var d=!1;angular.forEach(a.model.ProductCurrencies,function(a){a.CurrencyID==c.CurrencyID&&(d=!0)}),d||a.model.ProductCurrencies.push({CurrencyID:c.CurrencyID,CurrencyName:c.CurrencyName,ProductID:b.ProductID,Adding:!0})})}):(angular.forEach(a.cultures,function(b){a.model.ProductCultures.push({CultureID:b.CultureID,CultureName:b.Name,ProductName:"",Adding:!0})}),angular.forEach(a.currencies,function(b){a.model.ProductCurrencies.push({CurrencyID:b.CurrencyID,CurrencyName:b.CurrencyName,Adding:!0})}))},a.loadCategories=function(b){d.getCategories(b).then(function(b){a.categories=b})},a.addCategory=function(b){b.Adding=!0,b.Deleting=!1,a.model.ProductCategories.push(b),a.categories=[],a.queryCategories=""},a.removeCategory=function(a){a.Deleting=!0,a.Adding=!1},a.loadTags=function(b){d.getTags(b).then(function(b){a.tags=b})},a.addTag=function(b){b.Adding=!0,b.Deleting=!1,a.model.ProductTags.push(b),a.tags=[],a.queryTags=""},a.removeTag=function(a){a.Deleting=!0,a.Adding=!1},a.save=function(){null!=a.model.ProductCategories&&a.model.ProductCategories.length>0?d.save(a.model).then(function(b){if(a.model.ProductID=null==a.model.ProductID?b:a.model.ProductID,a.model.Files&&a.model.Files.length)for(var c=0;c<a.model.Files.length;c++){var e=a.model.Files[c];d.uploadGallery(a.model.ProductID,e).progress(function(a){parseInt(100*a.loaded/a.total)}).success(function(b,c,d,e){null==a.model.ProductGalleries&&(a.model.ProductGalleries=[]),a.model.ProductGalleries.push(b),a.model.Files=[]})}}):g.error("Tienes que agregar al menos una categoría")},a["delete"]=function(b){confirm("¿Estas seguro que deseas eliminar este producto?")&&d.remove(b.ProductID).then(function(b){a.get(),g.success("Producto Eliminado")})},a.deleteGallery=function(b){confirm("¿Estas seguro que deseas eliminar esta imagen?")&&d.removeGallery(b.ProductGalleryID).then(function(c){var d=a.model.ProductGalleries.indexOf(b);a.model.ProductGalleries.splice(d,1)})},a.init=function(){f.get().then(function(b){a.currencies=b,a.get()})},a.$on("initDone",function(b){a.init()}),e.sitesLoaded&&a.init()}]),app.controller("productsController",["$scope","productService","siteService","toastrService",function(a,b,c,d){a.get=function(){b.get().then(function(b){a.data=b})},a["delete"]=function(c){confirm("¿Estas seguro que deseas eliminar este producto?")&&b.remove(c.ProductID).then(function(b){a.get(),d.success("Producto Eliminado")})},a.options={dropped:function(a){var c=a.source.nodeScope,d=a.dest.index,e=c.$modelValue.ProductID;b.move(e,d).then(function(a){})}},a.init=function(){a.get()},a.$on("initDone",function(b){a.init()}),c.sitesLoaded&&a.init()}]),app.controller("tagController",["$scope","tagService","siteService","toastrService",function(a,b,c,d){a.get=function(){b.get().then(function(b){a.data=b})},a.open=function(b){null==b?a.model={}:a.model=angular.copy(b),$("#modal-new").modal({backdrop:!0})},a.save=function(){b.save(a.model).then(function(b){a.get()}),$("#modal-new").modal("hide")},a["delete"]=function(c){confirm("¿Estas seguro que deseas eliminar este Tag?")&&b.remove(c.TagID).then(function(b){a.get(),d.success("Tag Eliminado")})},a.init=function(){a.get()},a.$on("initDone",function(b){a.init()}),c.sitesLoaded&&a.init()}]),app.directive("autoFillSync",["$timeout",function(a){return{restrict:"A",require:"ngModel",link:function(a,b,c,d){a.$on("autofill:update",function(){d.$setViewValue(b.val())})}}}]),app.directive("fileread",[function(){return{scope:{fileread:"="},link:function(a,b,c){b.bind("change",function(b){var c=new FileReader;c.onload=function(c){a.$apply(function(){null==a.fileread&&(a.fileread={}),a.fileread.FileContent=c.target.result,a.fileread.FileName=b.target.files[0].name,a.fileread.Edit=!0,a.fileread.Deleting=!1})},c.readAsDataURL(b.target.files[0])})}}}]),app.directive("attrIf",[function(){return{restrict:"A",link:function(a,b,c){a.$watch(c.attrIf,function(a){for(var c in a)a[c]&&b.attr(c,!0)})}}}]),app.directive("tagManager",function(){return{restrict:"E",scope:{tags:"=",autocomplete:"="},templateUrl:"/app/directives/_tagManager.html",link:function($scope,$element,$attributes){var input=angular.element($element.children()[1]);$scope.add=function(value){null==value?eval("value = {"+$attributes.keyproperty+":0}"):$scope.new_value=eval("value."+$attributes.valueproperty);var _item=null;angular.forEach($scope.tags,function(item){eval("item."+$attributes.valueproperty)==$scope.new_value&&(_item=item)}),null==_item?eval("$scope.tags.push({"+$attributes.valueproperty+": $scope.new_value,Adding:true,Deleting:false,"+$attributes.keyproperty+":value."+$attributes.keyproperty+"})"):(_item.Adding=!0,_item.Deleting=!1),$scope.new_value="",$scope.tagList=null},$scope.getValue=function(item){return eval("item."+$attributes.valueproperty)},$scope.change=function(){$scope.new_value.length>1&&$scope.autocomplete($scope.new_value).then(function(a){$scope.tagList=a})},$scope.remove=function(a){null==a.Deleting?(a.Deleting=!0,a.Adding=!1):(a.Deleting=!a.Deleting,a.Adding=!a.Adding)},input.bind("keypress",function(a){13==a.keyCode&&($scope.$apply($scope.add(null)),a.preventDefault())})}}});