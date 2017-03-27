﻿// Copyright (c) Source Tree Solutions, LLC. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Author:					Joe Audette
// Created:					2015-09-05
// Last Modified:			2016-02-26
// 

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SF.Web.Navigation
{
    public class NavigationOptions
    {
        public NavigationOptions()
        { }

        public string RootTreeBuilderName { get; set; } = "SF.Web.Navigation.XmlNavigationTreeBuilder";

        public string NavigationMapJsonFileName { get; set; } = "navigation.json";
        public string NavigationMapXmlFileName { get; set; } = "navigation.xml";
    }
}
