﻿using System;
using System.Collections.Generic;
using System.Text;
using TestHouse.Domain.Enums;

namespace TestHouse.DTOs.DTOs
{
    public class TestRunCaseDto
    {
        /// <summary>
        /// Test run case id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// Test case
        /// </summary>
        public TestCaseDto TestCase { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public TestCaseStatus Status { get; set; }

        ///// <summary>
        ///// Steps
        ///// </summary>
        public IEnumerable<StepRunDto> Steps { get; set; }
    }

    
}
