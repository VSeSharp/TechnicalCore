﻿using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using TechnicalCore.Api.Data.Entities;

namespace TechnicalCore.Api.GraphQL.Messaging
{
    public class ReviewMessageService
    {
        private readonly ISubject<ReviewAddedMessage> _messageStream = new ReplaySubject<ReviewAddedMessage>(1);

        public ReviewAddedMessage AddReviewAddedMessage(ArticleReview review)
        {
            var message = new ReviewAddedMessage
            {
                ArticleId = review.ArticleId,
                Title = review.Title
            };
            _messageStream.OnNext(message);
            return message;
        }

        public IObservable<ReviewAddedMessage> GetMessages()
        {
            return _messageStream.AsObservable();
        }
    }
}
