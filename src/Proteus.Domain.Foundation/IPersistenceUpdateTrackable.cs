/*
 *
 * Proteus
 * Copyright (c) 2008 - 2011
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 * 
 *     http://www.apache.org/licenses/LICENSE-2.0
 * 
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 * 
 */ 



using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proteus.Domain.Foundation
{
    /// <summary>
    /// Indicates publicly-accessible persistence update tracking properties are available
    /// </summary>
    public interface IPersistenceUpdateTrackable <out TUpdater>
    {
        /// <summary>
        /// Gets the last updated date time.
        /// </summary>
        /// <value>The last updated date time.</value>
        DateTime LastUpdatedDateTime { get; }
        /// <summary>
        /// Gets the last updated by.
        /// </summary>
        /// <value>The last updated by.</value>
        TUpdater LastUpdatedBy { get; }
    }
}
