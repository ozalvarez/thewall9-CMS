# Welcome to thewall9 CMS

This is a Multilanguaje **Experimental Cloud CMS with Blog features** created with ASP.NET MVC 5.2.2, ASP.NET Web API 2, Entity Framework Code First, Identity 2.0, SQL Server on Azure, Azure Blobs, Angular.js, jQuery and Twitter Bootstrap.

Screenshots: [https://www.flickr.com/photos/129114186@N06/sets/72157650109877927/](https://www.flickr.com/photos/129114186@N06/sets/72157650109877927/)
## Why a Cloud CMS

There are a several reason to create this architecture, here some of them:

* There is 1 API solution for all websites, we have to maintain just one code environment
* Each Website is a kind of API Client making calls to the API Layer
* We can have different website using the same Web Project the only differentiation is `Request.Url.Authority` and the data return is different
* We can duplicate websites using the same template but different data
* We can create a site with just one click
* There is only 1 admin platform and only 1 customer platform so if we change any of it any customer will see the changes
* Customers can have many websites in 1 customer platform and with a drop-down they can change the website to configure
* For Developers: if they want to create a website they just have to know HTML, JavaScript, CSS and a few Razor HTML Helpers

## Status

This repository is a **Experimental Alpha CMS** your are welcome to contribute. Ecommerce features is under development.

We consider this repository a pre-release

## Technologies

* Database
    * Azure Database
* Files
    * Azure Blobs
* Back-end
    * Entity Framework Code First
    * ASP.NET Identity
    * Microsoft ASP.NET Web API
* Front-end
    * Angular.js
    * jQuery
    * Bootstrap
    * Grunt.js

## Installation CMS

1. Download the code and open the solution in Visual Studio
2. Go to project `thewall9.data` open the file `Migrations/Configuration.cs` to setting up your database
3. Change this values with your own data, this is the values of thr root or main user.
    
    ```
public const string EmailRoot = "[YOUR ROOT EMAIL]";
public const string NameRoot = "[YOUR ROOT NAME]";
```
4. Execute Migrations with the Package manager Console, be sure in the Package manager Console the project `thewall9.data` is selected and the **Startup Project** is `thewall9.api`
    ```
update-database
```
Now you have the database, you can change the location of the database in `Web.config` `thewall9.api`

5. Open Task Runner Explorer, select thewall9.admin and execute **build** task.
6. Press F5 or Start Debugging, be sure the **Startup Project** is `thewall9.api`. This will open a browser with this url `http://localhost:2497/` I call this URL `API_URL` all websites will point to this API URL. You can change this URL if you want.
7. Open in your IIS Express `thewall9.admin` website it will prompt for a username/email and a password. You will put `[YOUR ROOT EMAIL]` as a username/email and `123456` as a password. Now you are in admin portal.
8. Now you will create your first website, you will create a copy of [thewall9.com](http://thewall9.com) site.
    1. Find a file in this folder `/webs` called `SITE.json`
    2. Be sure you have installed and running **Windows Azure Storage Emulator - v3.4+** otherwise this don't work.
    3. In **Admin Portal** go to **Site** section and press **Import Site** button select the file you already locate called `SITE.json` and press the **Upload** button
    4. Now you have a copy of [thewall9.com](http://thewall9.com) site.
9. We going to test your copy of [thewall9.com](http://thewall9.com) site is working
	1. Go to the next repository https://github.com/ozalvarez/thewall9-web and open this solution
    2. Open in your IIS Express `thewall9.web` website
    3. Be sure in `thewall9.web` `Web.config` file the value of your **Site ID** is the same that in your **Admin Portal Sites** section `<add key="SiteID" value="1" />`

Now you have running **thewall9 CMS** in your computer. You are welcome to make contribution via **Pull Request**.

## Installation of Customer Portal
1. Open Task Runner Explorer, select thewall9.customer and execute **build** task.
2. Open Web.config, add this appSetting to point the customer portal to the API
    ```
<add key="API_URL" value="http://localhost:2497/" />
```
3. Press F5 or Start Debugging, be sure the **Startup Project** is `thewall9.customer`.

## Documentation

Is under development

## thewall9 Cloud CMS

I have a cloud solution running right now, if you want an access write me an email to [oz@thewall9.com](mailto:oz@thewall9.com) and i can create a site for you. you only have to clone a website project called `blank.web` in `webs` solution folder, be sure in Web.config `<add key="API_URL" value="https://api-thewall9.azurewebsites.net/" />` is set and you are ready to use the cloud solution

### Installation using the Cloud CMS

1. Create a Blank Website Project
2. Install from NUGET thewall9CMS in your website

    ```
Install-Package thewall9CMS -Pre
```

3. Find the file Global.asax and extend from `thewall9.web.parent.Thewall9Application`
4. Be sure you have the right values for this settings

    ```
<add key="SiteID" value="1" />
<add key="API_URL" value="http://localhost:2497/" />
```
5. If you want to create a site in the cloud platform write me an email to [info@thewall9.com](mailto:info@thewall9.com) and i will create a SiteID for you.

### Websites running in thewall9 Cloud CMS

* [http://thewall9.com/](http://thewall9.com/)
* [http://elcubo9.com/](http://elcubo9.com/)
* [http://bebsabeduque.com/](http://bebsabeduque.com/)
* [http://abogadacardenas.com/](http://abogadacardenas.com/)
* [http://workandgo.com/](http://workandgo.com/)
* [http://nazcaboutique.com/](http://nazcaboutique.com/)
* [http://theplayer9.com/](http://theplayer9.com/)
* ADD YOUR LINK HERE.

### Examples (Source Code)

* [http://thewall9.com/](http://thewall9.com/) https://github.com/ozalvarez/thewall9-web
