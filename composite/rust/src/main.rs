use std::mem::ManuallyDrop;

trait Component {
    fn execute(&self) -> u32;
}

#[derive(Debug, Clone)]
struct Leaf {
    content: u32,
}

impl Component for Leaf {
    fn execute(&self) -> u32 {
        self.content
    }
}

impl Leaf {
    pub fn new(content: u32) -> Self {
        Self { content }
    }
}

struct Node {
    left: Option<Box<NodeChild>>,
    right: Option<Box<NodeChild>>,
}

#[derive(Debug)]
enum NodeChildEnum {
    Child,
    Node,
}

union NodeChildUnion {
    node: std::mem::ManuallyDrop<Node>,
    child: std::mem::ManuallyDrop<Vec<Leaf>>,
}

struct NodeChild {
    node_type: NodeChildEnum,
    child: NodeChildUnion,
}

impl NodeChild {
    pub fn from_node(node: std::mem::ManuallyDrop<Node>) -> Box<Self> {
        Box::new(Self {
            node_type: NodeChildEnum::Node,
            child: NodeChildUnion { node },
        })
    }

    pub fn from_child(child: std::mem::ManuallyDrop<Vec<Leaf>>) -> Box<Self> {
        Box::new(Self {
            node_type: NodeChildEnum::Child,
            child: NodeChildUnion { child },
        })
    }
}

impl Component for NodeChild {
    fn execute(&self) -> u32 {
        unsafe {
            match self.node_type {
                NodeChildEnum::Node => self.child.node.execute(),
                NodeChildEnum::Child => self.child.child.iter().map(|x| x.execute()).sum(),
            }
        }
    }
}

impl Component for Node {
    fn execute(&self) -> u32 {
        let s1 = if let Some(left) = &self.left {
            left.execute()
        } else {
            0
        };

        let s2 = if let Some(right) = &self.right {
            right.execute()
        } else {
            0
        };

        s1 + s2
    }
}

impl Node {
    pub fn from_node_child(left: Option<Box<NodeChild>>, right: Option<Box<NodeChild>>) -> Self {
        Self { left, right }
    }

    pub fn append_left(&mut self, left: Box<NodeChild>) {
        self.left = Some(left);
    }
    pub fn append_right(&mut self, right: Box<NodeChild>) {
        self.right = Some(right);
    }
}

fn main() {
    let children1 = vec![Leaf::new(42), Leaf::new(1), Leaf::new(22), Leaf::new(72)];
    let node1 = Node::from_node_child(
        Some(NodeChild::from_child(ManuallyDrop::new(children1.clone()))),
        None,
    );

    let r1 = node1.execute();
    println!("R1: {r1}");

    let mut node2 = Node::from_node_child(
        Some(NodeChild::from_node(ManuallyDrop::new(node1))),
        Some(NodeChild::from_child(ManuallyDrop::new(children1))),
    );

    let r2 = node2.execute();
    println!("R2: {r2}");

    let mut node3 = Node::from_node_child(
        None,
        Some(NodeChild::from_child(ManuallyDrop::new(vec![
            Leaf::new(42),
            Leaf::new(42),
        ]))),
    );

    let node4 = Node::from_node_child(
        None,
        Some(NodeChild::from_child(ManuallyDrop::new(vec![
            Leaf::new(1),
            Leaf::new(2),
        ]))),
    );
    node3.append_left(NodeChild::from_node(ManuallyDrop::new(node4)));
    let r3 = node3.execute();
    println!("R3: {r3}");

    node2.append_right(NodeChild::from_node(ManuallyDrop::new(node3)));
    let r4 = node2.execute();
    println!("R4: {r4}");
}
