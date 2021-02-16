using NUnit.Framework;
using TestNinja.Fundamentals;

namespace TestNinja.UnitTests.Fundamentals
{
    [TestFixture]
    public class StackTests
    {
        [Test]
        public void Push_ArgumentIsNull_ThrowsArgumentNullException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Push(null), Throws.ArgumentNullException);
        }
        
        [Test]
        public void Push_ValidArgument_AddObjectToStack()
        {
            var stack = new Stack<string>();

            stack.Push("a");

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Count_StackIsEmpty_ReturnZero()
        {
            var stack = new Stack<string>();
 
            Assert.That(stack.Count, Is.EqualTo(0));
        }

        [Test]
        public void Pop_StackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Pop(), Throws.InvalidOperationException);
        }

        [Test]
        public void Pop_StackHasTwoItems_ReturnItemOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");

            var result = stack.Pop();

            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void Pop_StackHasTwoItems_RemoveObjectOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");

            stack.Pop();

            Assert.That(stack.Count, Is.EqualTo(1));
        }

        [Test]
        public void Peek_StackIsEmpty_ThrowsInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.That(() => stack.Peek(), Throws.InvalidOperationException);
        }

        [Test]
        public void Peek_StackHasTwoItems_ReturnObjectOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");

            var result = stack.Peek();

            Assert.That(result, Is.EqualTo("B"));
        }

        [Test]
        public void Peek_StackHasItems_DoesNotRemoveObjectOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("A");
            stack.Push("B");

            stack.Peek();

            Assert.That(stack.Count, Is.EqualTo(2));
        }
    }
}
