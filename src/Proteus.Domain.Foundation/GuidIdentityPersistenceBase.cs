/*
 *
 * Proteus
 * Copyright (C) 2008, 2009
 * Stephen A. Bohlen
 * http://www.unhandled-exceptions.com
 *
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 *
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * Lesser General Public License for more details.
 *
 * You should have received a copy of the GNU Lesser General Public
 * License along with this library; if not, write to the Free Software
 * Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA
 *
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Proteus.Domain.Foundation
{
    /// <summary>
    /// Base Class for Persistent Entities using GUID as the data type for their identity field.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Obsolete("The type GuidIdentityPersistenceBase<T> is depricated; use IdentityPersistenceBase<TObject, TIdentity> in its place.")]
    public class GuidIdentityPersistenceBase<T> : IdentityPersistenceBase<T, Guid>
    {
    }
}
