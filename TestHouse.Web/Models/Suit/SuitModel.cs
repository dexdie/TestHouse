﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestHouse.Web.Models.Suit
{
    public class SuitModel
    {
        /// <summary>
        /// Suit name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Suit description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Project Id
        /// </summary>
        [Required]
        public long ProjectId { get; set; }

        /// <summary>
        /// Parent suit id
        /// </summary>
        [Required]
        public long ParentId { get; set; }
    }
}
