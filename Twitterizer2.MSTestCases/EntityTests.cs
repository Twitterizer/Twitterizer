﻿//-----------------------------------------------------------------------
// <copyright file="EntityTests.cs" company="Patrick Ricky Smith">
//  This file is part of the Twitterizer library (http://www.twitterizer.net/)
// 
//  Copyright (c) 2010, Patrick "Ricky" Smith (ricky@digitally-born.com)
//  All rights reserved.
//  
//  Redistribution and use in source and binary forms, with or without modification, are 
//  permitted provided that the following conditions are met:
// 
//  - Redistributions of source code must retain the above copyright notice, this list 
//    of conditions and the following disclaimer.
//  - Redistributions in binary form must reproduce the above copyright notice, this list 
//    of conditions and the following disclaimer in the documentation and/or other 
//    materials provided with the distribution.
//  - Neither the name of the Twitterizer nor the names of its contributors may be 
//    used to endorse or promote products derived from this software without specific 
//    prior written permission.
// 
//  THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND 
//  ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED 
//  WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED. 
//  IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT, 
//  INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT 
//  NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR 
//  PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
//  WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) 
//  ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE 
//  POSSIBILITY OF SUCH DAMAGE.
// </copyright>
// <author>Ricky Smith</author>
// <summary>Test class for entities.</summary>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using Twitterizer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Twitterizer2.MSTestCases;
using Twitterizer.Entities;

namespace Twitterizer2.MSTestCases
{
    [TestClass]
    public class EntityTests
    {
        [TestMethod]
        public void LinkifyText()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var StatusResult = TwitterStatus.Show(tokens, 10506615291641857);
            Assert.IsNotNull(StatusResult, "Status result is null");
            Assert.IsTrue(StatusResult.Result == RequestResult.Success, StatusResult.ErrorMessage ?? StatusResult.Result.ToString());
            Assert.IsNotNull(StatusResult.ResponseObject, StatusResult.ErrorMessage ?? StatusResult.Result.ToString());

            if (StatusResult.ResponseObject.Entities == null)
                return;

            string linkedText = StatusResult.ResponseObject.Text;

            var entitiesSorted = StatusResult.ResponseObject.Entities.OrderBy(e => e.StartIndex).Reverse();

            foreach (TwitterEntity entity in entitiesSorted)
            {
                if (entity is TwitterHashTagEntity)
                {
                    TwitterHashTagEntity tagEntity = (TwitterHashTagEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/search?q=%23{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        tagEntity.Text,
                        linkedText.Substring(entity.EndIndex));
                }

                if (entity is TwitterUrlEntity)
                {
                    TwitterUrlEntity urlEntity = (TwitterUrlEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"{1}\">{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        urlEntity.Url,
                        linkedText.Substring(entity.EndIndex));
                }

                if (entity is TwitterMentionEntity)
                {
                    TwitterMentionEntity mentionEntity = (TwitterMentionEntity)entity;

                    linkedText = string.Format(
                        "{0}<a href=\"http://twitter.com/{1}\">@{1}</a>{2}",
                        linkedText.Substring(0, entity.StartIndex),
                        mentionEntity.ScreenName,
                        linkedText.Substring(entity.EndIndex));
                }
            }
        }

        [TestMethod]
        public void BuiltInLinkify()
        {
            OAuthTokens tokens = Configuration.GetTokens();

            var StatusResult = TwitterStatus.Show(tokens, 10506615291641857);
            Assert.IsNotNull(StatusResult, "StatusResult is null");
            Assert.IsTrue(StatusResult.Result == RequestResult.Success, StatusResult.ErrorMessage);
            Assert.IsNotNull(StatusResult.ResponseObject, StatusResult.ErrorMessage);

            /*string linkifiedText =*/ StatusResult.ResponseObject.LinkifiedText();
        }
    }
}
