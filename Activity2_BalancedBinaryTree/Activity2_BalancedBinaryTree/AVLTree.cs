using System;

namespace Activity2_BalancedBinaryTree
{

    /** Name: Robert Jacobs 30018755
     *  Date: In Progress
     *  Project: Creating an AVL tree to insert, find and delete string values.
     *  Description: Using online References I was able to create an AVLTree and populate it with values. The references were used to create the Insert and Delete functionality
     *  of the class but I was able to create the find and update functionality myself using what I learned. The user is taken through a quick tutorial to explore the different 
     *  functionality before entering a controlled loop where they can interact with the program. If time permits I will do more error handling if the user makes a mistake but currently
     *  the program works fine as long as the instructions are followed correctly.
     *  
     *  ***NOTE** both the AVL tree and the main method are on the same page.
     *  
     *   References were used to create the majority of the AVLTree Class. Anything not included on these webpages is my own work. All comments are my own work.
     *   https://www.geeksforgeeks.org/avl-tree-set-1-insertion/ - https://www.geeksforgeeks.org/avl-tree-set-2-deletion/
     */


    class Node // Node Class creates a new node to be inserted into the tree in the AVLTree Class.
    {
        public int key, height; // Determines index number and height in the tree.
        public Node left, right; // Allows the node to be located within the memory to form the tree.
        public string data;

        public Node(int d)
        {
            key = d; // The value of the node to allow the node to be assigned to a place and found later on.
            height = 1; // Keeps track of the nodes position in the tree.
        }
    }
    
    public class AVLTree
    {

        Node root;

        // Height allows the program to keep track of how high the tree is and how high or low each node is on the tree itself.
        int height(Node N)
        {
            if (N == null) 
                return 0;

            return N.height;
        }

        // Max is an updated height value. Determines how high the tree is by taking and comparing the two highest nodes.
        // Which ever node is "taller" in the tree has its value taken as the new max.
        int max(int a, int b)
        {
            // The "?" is a "Conditional Oporator". It works like a smaller if/else statement.
            // If the initial condition is met (a > b) then do the first condition on the left side (a). Vise versa if the condition is not met.
            // The a and b values refer to the height of different Node values (not the key) in the rotation methods (below).
            return (a > b) ? a : b;
        }

       // This method balances the tree by measuring the height of the nodes on the left side and comparing them to the right side of the tree.
       // If the left side is bigger then the tree will rotate right by allocating a node on the left side the position of root.
       // Giving the root value to a new node will cause the right side to drop down, left side to lift up causing a rebalancing affect.
        Node rightRotate(Node y) // The Y value is given to the current Root node at the bottom of the tree.
        {
            Node x = y.left; //Y's left Node Value (not the key) is assigned to the key value of X. 
            Node T2 = x.right; //X's right Node value (not the key) is assigned to the key value of T2.

            
            x.right = y; // The Key value of Y is assigned to the Right Node Value of X.
            y.left = T2; // The Key value of T2 is assigned to the Left Value of Y.

            // The total height of Y uses the Height values of its Children (Left and right) and picking the greater value.
            // Since Y needs to remain the parent it changes its value to equal the biggest child but then adding one to its own height.
            y.height = max(height(y.left),height(y.right)) + 1; 

            // The same is repeated with the X Node.
            // Since the Height Value of T2 (which is connected to X) replaces the current value of Y.left (above) the tree rotates so X becomes the new root.
            x.height = max(height(x.left),height(x.right)) + 1;

            // The values of Node X gets returned replacing the Values of Y as the new root.
            return x;
        }

        // Rotate left works the same as Rotate right but in reverse. Please refer to the code above for comment.
        Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            
            y.left = x;
            x.right = T2;

            
            x.height = max(height(x.left),height(x.right)) + 1;
            y.height = max(height(y.left),height(y.right)) + 1;

            // The values of Node Y gets returned replacing the Values of X as the new root.
            return y;
        }

        // The getBalance Looks at the height of a Node and gets one of 3 values. Either -1 , 0 or 1.
        // If the value is 1 then the node is imbalanced down the left side of the tree. If - 1 the node is imbalanced on the right side of the tree. 0 means the tree is currently balanced.
        int getBalance(Node N)
        {
            if (N == null)
                return 0; // the Node being null means that it is the last item in a tree and does not require any balancing beneath it.

            return height(N.left) - height(N.right); // Check to see if tree is balanced.
        }

        // The insert method creates a new Node, assigns it a key and compares it to other nodes to determine which place in the tree it will go to.
        // This method also rebalances the tree often calling the getBalance Method Discussed above.
        Node insert(Node node, int key)
        {

            if (node == null)
                return (new Node(key)); // Accepts a null node object, creates a new instance of a node and assigns it a key Integer value.

            if (key < node.key)// Compares the keys of the new node to the node currently occupying the left side of the root.
                node.left = insert(node.left, key); // If the key of the newly created node is less than the other node, then it is inserted on the left side.
            else if (key > node.key)
                node.right = insert(node.right, key); // If the key of the newly created node is more than the other node, then it is inserted on the right side.
            else  
                return node;

           
            node.height = 1 + max(height(node.left), height(node.right)); // The newly created node has its height assigned as higher than it's children (if any).

            int balance = getBalance(node); // The balance is checked. int balance is assigned either -1, 0 or + 1 depending on the conditions.

            //** See Delete Method for Full explanation on rotations**
            //** Main difference is the second condition is based on the boolean value of < / > rather than using the getBalance Method to provide three values (-1 , 0 , +1)
            
            if (balance > 1 && key < node.left.key)  
                return rightRotate(node);

           
            if (balance < -1 && key > node.right.key)
                return leftRotate(node);

          
            if (balance > 1 && key > node.left.key)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            
            if (balance < -1 && key < node.right.key)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }
            // Below are my own additions to expand the tree to contain 15 Nodes.

            return node; // Returns the newly created node with the correct values.
        }
        Node minValueNode(Node node)
        {
            Node current = node; // Creates a new node object and assigns it the values of the current node (the searched item in the deleteNode function).

            
            while (current.left != null) // Seeks the left most value (lowest value). The lowest value will have no objects on it's left side.
                current = current.left; // Assigns the current object as the smallest Node in the tree. Making it the minValueNode.

            return current;
        }


        // Delete Function allows the program to find a specific node based on its key and then delete it.
        Node deleteNode(Node root, int key)
        {
            
            if (root == null) // If root is null then ignore it and exit out of method.
                return root;

            // If the searched key is smaller than the root key then the target node is in the left side.
            if (key < root.key)
                root.left = deleteNode(root.left, key);

           // If the searched key is smaller than the root key then the target node is in the right side.
            else if (key > root.key)
                root.right = deleteNode(root.right, key);

           
            else // If the previous two statements did not work then the target is the current root and that will be deleted.
                // Note to avoid other nodes from being disconnected the target must be isolated and deleted seperately.
            {

                // Checks if current root has children on either left or right side. This would mean that the root has been isolated or it's deletion wont affect any other nodes.
                if ((root.left == null) || (root.right == null))
                {
                    Node temp = null; // Creates a temp node that is null. This will replace any values causing them to be deleted.
                    if (temp == root.left) // If left is null replace the right value with a null value. Thus deleting the record.
                        temp = root.right;
                    else
                        temp = root.left; // If the left value is not null then make it null.

                     
                    if (temp == null) // Makes the root Null to end the deletion process.
                    {
                        temp = root;
                        root = null;
                    }
                    else
                        root = temp;               
                }
                else // If the target has children nodes then it has to be swapped with the lowest valued node before being deleted. 
                    //This will ensure the rest of the nodes will be kept away from the deletion process.
                {
                    Node temp = minValueNode(root.right); // The right value of the root will be the successor to the previous root after deletion.

                    root.key = temp.key; // The root key is assigned the temp values (previously the roots own children) promoting the child to root.

                    root.right = deleteNode(root.right, temp.key); // After swapping the values of the root and child. The new Child (previously the root) is now deleted.
                }
            }

            
            if (root == null) // If root is null ignore it and exit out of method.
                return root;

             
            root.height = max(height(root.left), // After the new root is promoted and the previous root is deleted the tree must be updated and sorted.
                        height(root.right)) + 1;

            int balance = getBalance(root); // The balance of the current root is checked. int balance is assigned either -1, 0 or + 1 depending on the conditions.
                                            // If -1 is assigned the left side is unbalanced. If +1 is assigned the right side in unbalanced. If 0 there is no imbalance.

            // Two conditions must be met in order to allow a rotation to happen (represented in each if statements below).
            // Condition 1 (is it imbalanced?) = The balance cannot be zero. If any other number is present then there is an imbalance somewhere in the tree.
            // Condition 2 (where is the imbalance?)  = Checks the getBalance values of each node to check if the current node is where the tree is imbalanced.
           

            /**
             *                root                      // Root gets changed to match the middle value of the entire tree.
             *           /           \
             *       L Case           R Case            // This line can be defined from the root and checked. The rotation happens on this line.
             *     /        \         /        \
             * (LL Case) (LR Case) (RL Case) (RR Case) // The imbalance will be present on this line.
             * 
             * 
             * */

            // Left Left Case
            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            // Left Right Case  
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case  
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case  
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
            
        }

        // This Method was referenced and altered from the Delete Method. 
        // It allows the user to find and print the details of a node based on the key matching up to their input.
        Node findNode(Node root, int key)
        {

            if (root == null) // If root is null then ignore it and exit out of method.
                return root;

            // If the searched key is smaller than the root key then the target node is in the left side.
            if (key < root.key)
                root.left = findNode(root.left, key);

            // If the searched key is smaller than the root key then the target node is in the right side.
            else if (key > root.key)
                root.right = findNode(root.right, key);


            else // If the previous two statements did not work then the target is the current root and that will be Printed as the result.

            {
                if (root.key == key) // Checks if the current key matches the target.
                {
                    Console.WriteLine("Target Found - " + "key: " + root.key + "  height: " + root.height);
                    

                }
                else
                {
                    Console.WriteLine("Target not found"); // ***Not yet implemented properly***
                }

            }

            if (root == null) // If root is null ignore it and exit out of method.
                return root;

            root.height = max(height(root.left), // After the new root is promoted and the previous root is deleted the tree must be updated and sorted.
                        height(root.right)) + 1;

            int balance = getBalance(root); // The balance of the current root is checked. int balance is assigned either -1, 0 or + 1 depending on the conditions.

            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            //**Refer to delete method on explanation on how the rotation works**

            // Left Right Case  
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case  
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case  
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
        }

        // This Method was referenced and altered from the Delete / Find method.
        // It allows the user to select a node based on its key and to update it with a String Value that represents the data it will carry.
        Node updateNode(Node root, int key)
        {

            if (root == null) // If root is null then ignore it and exit out of method.
                return root;

            // If the searched key is smaller than the root key then the target node is in the left side.
            if (key < root.key)
                root.left = updateNode(root.left, key);

            // If the searched key is smaller than the root key then the target node is in the right side.
            else if (key > root.key)
                root.right = updateNode(root.right, key);


            else // If the previous two statements did not work then the target is the current root and that will be Printed as the result.

            {
                if (root.key == key) // Checks if the current key matches the target and asks the user to input values.
                {
                    Console.WriteLine("Target Found - " + "key: " + root.key + "  height: " + root.height);
                    Console.WriteLine("Please enter the string you wish to save into the node:");
                    root.data = Console.ReadLine();
                    Console.WriteLine("Record has been updated - Key : " + root.key + "  height: " + root.height + "  data: " + root.data);

                }
                else
                {
                    Console.WriteLine("Target not found , Please try again"); // *** Not yet implemented correctly***
                }

            }

            if (root == null) // If root is null ignore it and exit out of method.
                return root;

            root.height = max(height(root.left), // After the new root is promoted and the previous root is deleted the tree must be updated and sorted.
                        height(root.right)) + 1;

            int balance = getBalance(root); // The balance of the current root is checked. int balance is assigned either -1, 0 or + 1 depending on the conditions.

            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            //**Refer to delete method on explanation on how the rotation works**

            // Left Right Case  
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case  
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case  
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
        }
        
        // This Method prints out the current condition of the table.
        void printResults(Node node)
        {
            if(node != null)
            {
                Console.WriteLine("Node key - " + node.key + "  Node Height - " + node.height + "  Node Data - " + node.data);
                // Calls itself to print off the rest of the tree down both sides.
                printResults(node.left); 
                printResults(node.right);
            }
        }

        // Runs the tutorial for the application.
        public void tutorial()
        {
            int inputNumber;

            AVLTree tutTree = new AVLTree(); // Seperate Tree created to avoid any possible conflicts with the application.

            //Initial Run of the program takes the user through all the features as an initial presentation.

            // For loop to create nodes and populate them throughout tree.
            Console.WriteLine();
            Console.WriteLine("You have selected the Tutorial. Please follow the instructions so that you can see all the features.");
            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();

            Console.WriteLine("A brand new Tree shall be created and populated. *Note the data shall be added later*");
            Console.WriteLine();
            for (int i = 0; i < 15; i++)
            {
                tutTree.root = tutTree.insert(tutTree.root, i);
                Console.WriteLine("Node key - " + i + "  Node Height - " + tutTree.root.height + "  Node Data - " + tutTree.root.data);

            }

            Console.WriteLine("Press Enter to continue.");
            Console.ReadLine();
            Console.WriteLine();
            Console.WriteLine("The Tree will now be balanced -");
            tutTree.printResults(tutTree.root);

            // Simulates the deletion of an entry from the Tree and Rebalancing.
            Console.WriteLine();
            Console.WriteLine("Each Node carries a 'key' to identify it. You can delete the entry by inputting the key below.");
            Console.WriteLine("Please input the key (number) you wish to delete? : ");
            inputNumber = Convert.ToInt32(Console.ReadLine());
            tutTree.root = tutTree.deleteNode(tutTree.root, inputNumber);
            Console.WriteLine("The Node with the key " + inputNumber + "  is no longer visible. All the details of that node have been deleted.");
            tutTree.printResults(tutTree.root);

            //Simulates the finding of a particular node.
            Console.WriteLine();
            Console.WriteLine("The key can also be used to find different nodes. Input the key of the node you wish to find and print.");
            Console.WriteLine("Please input the key (number) of the node you wish to find : ");
            inputNumber = Convert.ToInt32(Console.ReadLine());
            tutTree.root = tutTree.findNode(tutTree.root, inputNumber);
            tutTree.printResults(tutTree.root);

            // Simulates the Updating of a new node with a String Value.
            Console.WriteLine();
            Console.WriteLine("Data can be saved to each node to create a database (of sorts). Use the key to select a Node and then Input the String you wish to save into the node.");
            Console.WriteLine("Please input the key (number) of the node you wish to update");
            inputNumber = Convert.ToInt32(Console.ReadLine());
            tutTree.root = tutTree.updateNode(tutTree.root, inputNumber);
            tutTree.printResults(tutTree.root);

        }

        // Runs the application at the users request.
        public void application()
        { // After the presentation the user is invited to use the program as a loop to carry out multiple different tasks.
            int inputNumber;

            AVLTree appTree = new AVLTree(); // New tree object created to test my own values being inserted. This is used to answer the Portfolio questions.
            Console.WriteLine();
            Console.WriteLine("You have selected to enter the application.");
            Console.WriteLine("Please press 'y' to continue the applications use. AnyKey to exit");
            char userChoice = Convert.ToChar(Console.ReadLine());
            if (userChoice == 'y')
            {
                bool loop = true;
                while (loop == true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter the command you wish to carry out - Insert (new) = 'i' -- Delete (existing) = 'd' -- Find (existing) = 'f' -- Update (existing) = 'u' -- Exit = Press Enter");
                    userChoice = Convert.ToChar(Console.ReadLine());
                    switch (userChoice)
                    {
                        case 'i': // Same as the insert from tutorial.
                            Console.WriteLine();
                            Console.WriteLine("Please Input the key (number) of the new node :");
                            inputNumber = Convert.ToInt32(Console.ReadLine());
                            appTree.root = appTree.insert(appTree.root, inputNumber);
                            appTree.printResults(appTree.root);
                            break;

                        case 'd':// Same as the delete from tutorial.
                            Console.WriteLine();
                            Console.WriteLine("Please input the key (number) you wish to delete? : ");
                            inputNumber = Convert.ToInt32(Console.ReadLine());
                            appTree.root = appTree.deleteNode(appTree.root, inputNumber);
                            appTree.printResults(appTree.root);
                            break;

                        case 'f':// Same as the find from tutorial.
                            Console.WriteLine();
                            Console.WriteLine("Please input the key (number) of the node you wish to find : ");
                            inputNumber = Convert.ToInt32(Console.ReadLine());
                            appTree.root = appTree.findNode(appTree.root, inputNumber);
                            appTree.printResults(appTree.root);
                            break;

                        case 'u':// Same as the update from tutorial.
                            Console.WriteLine();
                            Console.WriteLine("Please input the key (number) of the node you wish to update");
                            inputNumber = Convert.ToInt32(Console.ReadLine());
                            appTree.root = appTree.updateNode(appTree.root, inputNumber);
                            appTree.printResults(appTree.root);
                            break;

                        default:
                            loop = false;
                            break;
                    }
                }

            }
            else
            {
                Console.WriteLine("Exiting Program now.");
            }

        }

        public static void Main(String[] args)
        {
            
            AVLTree rTree = new AVLTree(); // New tree object created to test my own values being inserted. This is used to answer the Portfolio questions.
            bool loop = true;
            while (loop == true)
            {
                Console.WriteLine("Thank you for using the application. There is a tutorial present to showcase the features. Would you like to see the tutorial?");
                Console.WriteLine("To see Tutorial press 'y', to skip tutorial press 'n', to exit program click 'anyOtherkey'.");
                char userChoice = Convert.ToChar(Console.ReadLine());
                if (userChoice == 'y')
                {
                    rTree.tutorial();
                }
                else if (userChoice == 'n')
                    rTree.application();
                else
                {
                    Console.WriteLine("Exiting Program now.");
                    loop = false;
                }
            }

        }
    }
}
