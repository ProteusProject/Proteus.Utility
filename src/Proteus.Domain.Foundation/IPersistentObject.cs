/*
 *
 * Proteus
 * Copyright (C) 2008, 2009, 2010, 2011
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
    /// High-level persistent object interface, indicates support for the entire collection of persistence interfaces
    /// </summary>
    public interface IPersistentObject : IPersistenceIdentityField, IPersistenceVersionField, IPersistenceUpdateTrackable
    {
        /// <summary>
        /// Equalses the specified other.
        /// </summary>
        /// <param name="other">The other.</param>
        /// <returns></returns>
        bool Equals(object other);
        /// <summary>
        /// Gets the hash code.
        /// </summary>
        /// <returns></returns>
        int GetHashCode();
        /// <summary>
        /// Gets a value indicating whether this instance is transient.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is transient; otherwise, <c>false</c>.
        /// </value>
        bool IsTransient { get; }
    }
}
