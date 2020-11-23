# Platformus 2.0.0-alpha1

[![Join the chat at https://gitter.im/Platformus/Platformus](https://badges.gitter.im/Join%20Chat.svg)](https://gitter.im/Platformus/Platformus?utm_source=badge&utm_medium=badge&utm_campaign=pr-badge&utm_content=badge)

![Platformus logotype](http://platformus.net/platformus_github_icon.png)

## Introduction

Platformus is free, open source and cross-platform CMS based on ASP.NET Core and
[ExtCore framework](https://github.com/ExtCore/ExtCore). It is built using the best and the most
modern tools and languages (Visual Studio 2019, C# etc). Join our team!

### Few Facts About Platformus

1. It is free and open source.
2. It runs on Windows, Mac and Linux.
3. It is completely modular and extendable. Using the features of the underlying
[ExtCore framework](https://github.com/ExtCore/ExtCore) you can easily create your own extensions
to extend its functionality.
4. It is multicultural and multilingual.
5. It is fast, flexible and easy to use. You can describe even complicated entities and their relationships
without writing any code!

### Basic Concepts

With the full set of the extensions Platformus is object-oriented CMS and object is the central unit of its
data model. Objects can be standalone and embedded. While standalone object can be accessed via URL (using
some specified view), embedded object can only be used as the part of others.

Each object consists of properties and relations and is described by its class. Classes describe properties and
relations of the objects with the members. Each member has code, name, data type (for properties) or class (for
relations). In addition, with the data sources, classes describe which objects are to be loaded together with
the object.

For example, let’s say we have Developer class and Team class. Also, we can have Contact class too. Each
developer should have first name and last name properties and one relation to the object of class Team and one
or many relations to the objects of class Contact.

### Backend Screenshots

![List of objects](http://platformus.net/files/list_of_objects.png)
*List of objects*

![Edit object](http://platformus.net/files/edit_object.png)
*Edit object*

![List menus](http://platformus.net/files/list_menus.png)
*List menus*

## Getting Started

### Samples

Please take a look at our samles on GitHub:

* [Platformus-Sample-Personal-Website ](https://github.com/Platformus/Platformus-Sample-Personal-Website );
* [Platformus-Sample-Personal-Blog ](https://github.com/Platformus/Platformus-Sample-Personal-Blog );
* [Platformus-Sample-Ecommerce ](https://github.com/Platformus/Platformus-Sample-Ecommerce ).

These samples contain everything you need to run Platformus-based web application from Visual Studio 2019, including SQLite
database with the test data (and SQL scripts for other DB types).

### Tutorials

We have written [several tutorials](http://docs.platformus.net/en/latest/getting_started/index.html)
to help you start developing your Platformus-based web applications.

## Links

Live demo: http://demo.platformus.net/

Website: http://platformus.net/

Docs: http://docs.platformus.net/

Author: http://sikorsky.pro/