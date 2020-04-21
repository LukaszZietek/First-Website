using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace Services
{
    public static class GalleryImages
    {
        public static readonly ReadOnlyCollection<ImageDimension> GalleryDimensionList = new ReadOnlyCollection<ImageDimension>(
           new[] { new ImageDimension("large",900,600),
                    new ImageDimension("small",200,200)}

            );


    }
}